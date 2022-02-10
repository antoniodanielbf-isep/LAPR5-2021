import {Component, NgZone, OnInit} from '@angular/core';
import {UtilizadorService} from "../../services/utilizador-service/utilizador.service";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {pesquisarUtilizadorService} from "../../services/pesquisar-utilizador-service/pesquisar-utilizador.service";
import IUtilizadorDTO from "../../dto/utilizador-dto/IUtilizadorDTO";
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import IUtilizadorLikeDislikeDTO from "../../dto/utilizador-dto/IUtilizadorLikeDislikeDTO";
import IUtilizadorOrDestDTO from "../../dto/utilizador-dto/IUtilizadorOrDestDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-pesquisar-utilizador',
  templateUrl: './pesquisar-utilizador.component.html',
  styleUrls: ['./pesquisar-utilizador.component.css']
})
export class PesquisarUtilizadorComponent implements OnInit {

  validPedidoLigacaoLinks = [];
  private userInfo: IAuthDTO = {email: ''};

  constructor(public fb: FormBuilder,
              private ngZone: NgZone,
              private router: Router,
              public usrService: UtilizadorService,
              public pedLigService: pesquisarUtilizadorService,
              private notification: MatSnackBar) {
  }

  utilizadoresPesquisados!: IUtilizadorDTO[] | undefined;

  pesquisaForm = new FormGroup({
    emailD: new FormControl('', Validators.required),
    cidadePaisD: new FormControl('', Validators.required),
    nomeD: new FormControl('', Validators.required)
  })

  pedidoLigacaoForm = new FormGroup({
    dest: new FormControl('', Validators.required),
    forca: new FormControl('', Validators.required),
    tagsL: new FormControl('', Validators.required)
  })

  ngOnInit(): void {
    this.getValidLinks();
    this.userInfo = this.usrService.userInfo;
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

  forcaLigacao: IUtilizadorLikeDislikeDTO = {valor: 0};
  users: IUtilizadorOrDestDTO = {emailOrigem: '', emailDestino: ''};

  irParaInicio() {
    this.router.navigate(['/inicio']);
  }

  submeterFormPesquisa(utilizadorPesquisadoEmail: string) {
    this.pedidoLigacaoForm.value.dest = utilizadorPesquisadoEmail;
    this.mostrarNotificacao('O meu pedido de ligação: \n Destinatário:' + this.pedidoLigacaoForm.value.dest + '\n Força:' + this.pedidoLigacaoForm.value.forca
      + '\n Tags:' + this.pedidoLigacaoForm.value.tagsL,false);
    alert('O meu pedido de ligação: \n Destinatário:' + this.pedidoLigacaoForm.value.dest + '\n Força:' + this.pedidoLigacaoForm.value.forca
      + '\n Tags:' + this.pedidoLigacaoForm.value.tagsL);
    this.pedLigService.createPedidoLigacao(this.pedidoLigacaoForm.value).subscribe(res => {
      this.mostrarNotificacao("Pedido de Ligacao bem Sucessido!",false);
    });
  }

  async pesquisarUtilizador() {
    await this.pedLigService.searchUtilizador(this.pesquisaForm.value).subscribe(async res => {
      this.utilizadoresPesquisados = res;
      if (res == null || res.length == 0) {
        this.mostrarNotificacao("Nada encontrado!", true)
      }

      for (let user of res) {
        this.users.emailOrigem = this.usrService.userInfo.email;
        this.users.emailDestino = user.email;
        await this.usrService.calcularForcaLigacao(this.users).subscribe(async res => {
          this.forcaLigacao = res;
          if (res == null) {
            this.mostrarNotificacao("Nada encontrado!", true)
          }
        })
      }
    });
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
