import {Component, NgZone, OnInit} from '@angular/core';
import IUtilizadorDTO from "../../dto/utilizador-dto/IUtilizadorDTO";
import IUtilizadorSugestoesDTO from "../../dto/utilizador-dto/IUtilizadorSugestoesDTO";
import {UtilizadorService} from "../../services/utilizador-service/utilizador.service";
import {PedirIntroducaoService} from "../../services/pedido-intro-service/pedir-introducao.service";
import {Router} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {pesquisarUtilizadorService} from "../../services/pesquisar-utilizador-service/pesquisar-utilizador.service";
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import {MatSnackBar} from "@angular/material/snack-bar";
import {RedeSocialService} from "../../services/rede-social-service/RedeSocialService";
import {GrupoDTO} from "../../dto/utilizador-dto/GrupoDTO";

@Component({
  selector: 'app-escolher-utilizadores-objetivo',
  templateUrl: './escolher-utilizadores-objetivo.component.html',
  styleUrls: ['./escolher-utilizadores-objetivo.component.css']
})
export class EscolherUtilizadoresObjetivoComponent implements OnInit {

  utilizadorSugerido: IUtilizadorDTO = {
    nomeUtilizador: '',
    breveDescricaoUtilizador: '',
    email: '',
    numeroDeTelefoneUtilizador: '',
    dataDeNascimentoUtilizador: '',
    perfilFacebookUtilizador: '',
    perfilLinkedinUtilizador: '',
    estadoEmocionalUtilizador: 0,
    tagsUtilizador: '',
    urlImagem: '',
    cidadePaisResidencia: '',
    dataModificacaoEstado: '',
    passwordU: ''
  }

  listaUtilizadoresSugeridos: IUtilizadorSugestoesDTO[] = [];

  validPedidoLigacaoLinks = [];
  private userInfo: IAuthDTO = {email: ''};

  sugestoesGrupos:GrupoDTO= {listaUsers:[]};
  sugerirGrupos:boolean=false;

  sugerirGruposForm = new FormGroup({
    tagsGrupos: new FormControl('', Validators.required),
    nrMinUtilizadores: new FormControl('', Validators.required),
    nrTagsComuns: new FormControl('', Validators.required)
  })

  constructor(public utilizadorService: UtilizadorService,
              public pedIntroService: PedirIntroducaoService,
              public pedLigService: pesquisarUtilizadorService,
              public redeSocialService: RedeSocialService,
              private ngZone: NgZone, private router: Router,
              private notification: MatSnackBar) {
  }

  ngOnInit(): void {
    this.getValidLinks();
    this.userInfo = this.utilizadorService.userInfo;

    this.utilizadorService.getSugestoesUtilizadores().subscribe(res => {
      this.listaUtilizadoresSugeridos = res;

      this.mostrarNotificacao('Carregando as sugestões de ligação iniciais!', false);
      if (res == null) {
        this.mostrarNotificacao('IMPOSSÍVEL SUGERIR!', true);
      }
    });

  }

  pedidoLigacaoForm = new FormGroup({
    dest: new FormControl('', Validators.required),
    forca: new FormControl('', Validators.required),
    tagsL: new FormControl('', Validators.required)
  })

  irParaInicio() {
    this.router.navigate(['/inicio']);
  }

  private getValidLinks() {
    for (const link of this.navPedidoLigacaoLinks) {
      // @ts-ignore
      this.validPedidoLigacaoLinks.push(link);
    }
  }

  navPedidoLigacaoLinks = [{
    path: 'pesquisar-utilizador',
    label: 'Create',
    icon: 'add_circle_outline',
    roles: ['Client', 'Administrator', 'Manager']
  }]

  submeterFormPesquisa(sugestaoAmizade: string) {
    this.pedidoLigacaoForm.value.dest = sugestaoAmizade;
    this.pedLigService.createPedidoLigacaoComSugestaoAmizade(this.pedidoLigacaoForm.value).subscribe(res => {
      if (res != null) {
        this.mostrarNotificacao("PedidoLigacao feito com sucesso!", false);
        for (let utilizadorSugerido of this.listaUtilizadoresSugeridos) {
          if (this.utilizadorSugerido.email == sugestaoAmizade) {
            this.removerDaLista(utilizadorSugerido);
          }
        }

      } else {
        this.mostrarNotificacao("PedidoLigacao sem sucesso!", true);

      }
    });
  }

  //remove o item da lista -> splice remove a partir do item passado por parâmetro 1 item, que será o próprio item
  removerDaLista(utilizadorSugerido: IUtilizadorSugestoesDTO) {
    this.listaUtilizadoresSugeridos.splice(this.listaUtilizadoresSugeridos.indexOf(utilizadorSugerido), 1);
  }

  submeterFormSugerirGrupos(){
    this.redeSocialService.sugerirGrupos(this.sugerirGruposForm.value.tagsGrupos,
      this.sugerirGruposForm.value.nrMinUtilizadores,
      this.sugerirGruposForm.value.nrTagsComuns)
      .subscribe(res=>{
        if(res!=null){
          this.sugestoesGrupos=res;
          this.sugerirGrupos=true;
        }

    })
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }

}
