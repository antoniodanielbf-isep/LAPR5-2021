import {Component, NgZone, OnInit} from '@angular/core';
import {PedirIntroducaoService} from "../../services/pedido-intro-service/pedir-introducao.service";
import {Router} from '@angular/router';
import {UtilizadorService} from "../../services/utilizador-service/utilizador.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import IPedidoIntroCriadoDTO from "../../dto/pedido-intro-dto/IPedidoIntroCriadoDTO";
import {IIntermediarioDTO} from "../../dto/pedido-intro-dto/IIntermediarioDTO";
import {catchError} from "rxjs";
import IUtilizadorDTO from "../../dto/utilizador-dto/IUtilizadorDTO";
import IUtilizadoresSugestoesDTO from "../../dto/utilizador-dto/IUtilizadorSugestoesDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-pedir-introducao-utilizador-objetivo',
  templateUrl: './pedir-introducao-utilizador-objetivo.component.html',
  styleUrls: ['./pedir-introducao-utilizador-objetivo.component.css']
})


export class PedirIntroducaoUtilizadorObjetivoComponent implements OnInit {

  listaUtilizadoresSugeridos: IUtilizadoresSugestoesDTO[] = [];

  pedidoIntroducao: IPedidoIntroCriadoDTO = {
    descricao: '', emailIntermedio: '', emailDestino: '',
    estadoPedidoIntroducao: 0, forca: 0, tags: ''
  }

  utilizadorIntermediario: IIntermediarioDTO | undefined = {
    email: ''
  };

  pedidoIntroducaoFeito: boolean = false;

  constructor(public utilizadorService: UtilizadorService,
              public pedIntroService: PedirIntroducaoService,
              private ngZone: NgZone, private router: Router,
              private notification: MatSnackBar
  ) {
  }

  ngOnInit(): void {
    //carregar sugestões de utilizadores objetivo iniciais
    this.utilizadorService.getSugestoesUtilizadoresParaPedirIntroducao().subscribe(res => {
      this.listaUtilizadoresSugeridos = res;
      this.mostrarNotificacao('Carregando as sugestões de ligação iniciais!', false);
      alert();
      if (res == null) {
        this.mostrarNotificacao('IMPOSSÍVEL SUGERIR!', true);
      }
      if (res != null && res.length == 0) {
        this.mostrarNotificacao('NENHUMA SUGESTÃO ENCONTRADA!', true);
      }
    });
  }

  pedidoIntroForm = new FormGroup({
    descricao: new FormControl('', Validators.required),
    forca: new FormControl('', Validators.required),
    listTags: new FormControl('', Validators.required)
  });

  /*//remove o item da lista -> splice remove a partir do item passado por parâmetro 1 item, que será o próprio item
  removerDaLista(utilizadorSugerido: IUtilizadorDTO) {
    this.listaUtilizadoresSugeridos.splice(this.listaUtilizadoresSugeridos.indexOf(utilizadorSugerido), 1);
  }*/

  emailDestinoForm = new FormGroup({
    emailDestino: new FormControl('', Validators.required)
  });

  irParaInicio() {
    this.router.navigate(['/inicio']);
  }

  getUtilizadorIntermediarioEFazerPedidoIntro() {

    const that = this;

    this.pedIntroService.getUtilizadorIntermediario
    (this.emailDestinoForm.value.emailDestino).then(function (y) {

      console.log(y);
      that.utilizadorIntermediario = y;
    }).catch(function (z) {
        alert('UTILIZADOR INTERMEDIÁRIO INVÁLIDO!')
      }
    );

    this.utilizadorIntermediario = that.utilizadorIntermediario;

    if (this.utilizadorIntermediario?.email != '') {
      this.pedidoIntroducaoFeito = true;
    }

  }

  submitFormPedidoIntroducao() {
    //estadoIntroducao ->  Pendente
    this.pedidoIntroducao.estadoPedidoIntroducao = 2;
    this.pedidoIntroducao.forca = this.pedidoIntroForm.value.forca;
    this.pedidoIntroducao.emailDestino = this.emailDestinoForm.value.emailDestino;
    this.pedidoIntroducao.descricao = this.pedidoIntroForm.value.descricao;
    this.pedidoIntroducao.emailIntermedio = <string>this.utilizadorIntermediario?.email;
    this.pedidoIntroducao.tags = this.pedidoIntroForm.value.listTags;


    this.pedIntroService.createPedidoIntroducao(this.pedidoIntroducao).subscribe(res => {

      if (res != null) {
        this.mostrarNotificacao('Pedido de Introdução feito com sucesso!', false);
        this.router.navigate(['/inicio']);
      }

    });
    this.router.navigate(['/inicio']);
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }

}
