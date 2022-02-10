import {Component, NgZone, OnInit} from '@angular/core';
import {PedirIntroducaoService} from "../../services/pedido-intro-service/pedir-introducao.service";
import IPedidoIntroDTO from "../../dto/pedido-intro-dto/IPedidoIntroDTO";
import {Router} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {IPedidoIntroAceiteDTO} from "../../dto/pedido-intro-dto/IPedidoIntroAceiteDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-aprovar-desaprovar-pedido-introducao',
  templateUrl: './aprovar-desaprovar-pedido-introducao.component.html',
  styleUrls: ['./aprovar-desaprovar-pedido-introducao.component.css']
})
export class AprovarDesaprovarPedidoIntroducaoComponent implements OnInit {

  listaPedidosIntroducaoPendentes: IPedidoIntroDTO[] = [];
  pedidoIntroducaoAceite: boolean = false;
  pedidoAceite: IPedidoIntroAceiteDTO = {
    id: '',
    descricaoPedido: '',
    emailOrigem: '',
    emailIntermedio: '',
    emailDestino: '',
    estadoPedidoIntroducao: 1, //1 significa aceite,0 recusado,2 pendente
    forca: 1,
    tags: '',
    descricaoIntroducao: ''
  };

  pedidoIntroAceiteForm = new FormGroup({
    descricao: new FormControl('', Validators.required)
  });

  //remove o item da lista -> splice remove a partir do item passado por parâmetro 1 item, que será o próprio item
  removerDaLista(pedidoIntroducaoPendente: IPedidoIntroDTO) {
    this.listaPedidosIntroducaoPendentes.splice(this.listaPedidosIntroducaoPendentes.indexOf(pedidoIntroducaoPendente), 1);
  }

  constructor(public pedidoService: PedirIntroducaoService,
              private ngZone: NgZone, private router: Router,
              private notification: MatSnackBar
  ) {
  }

  ngOnInit(): void {
    this.pedidoService.getPedidosIntroducaoPendentesUtilizador().subscribe((listaPedidos) => {
      this.listaPedidosIntroducaoPendentes = listaPedidos;
    });
    if (this.listaPedidosIntroducaoPendentes == []) {
      this.mostrarNotificacao('NENHUM PEDIDO DE INTRODUÇÃO ENCONTRADO!', true)
    }
  }

  irParaInicio() {
    this.router.navigate(['/inicio']);
  }

  aceitarPedidoIntroducao(pedidoIntroducaoPendente: IPedidoIntroDTO) {

    this.pedidoAceite.id = pedidoIntroducaoPendente.id;
    this.pedidoAceite.descricaoPedido = pedidoIntroducaoPendente.descricao;
    this.pedidoAceite.emailOrigem = pedidoIntroducaoPendente.emailOrigem;
    this.pedidoAceite.emailIntermedio = pedidoIntroducaoPendente.emailIntermedio;
    this.pedidoAceite.emailDestino = pedidoIntroducaoPendente.emailDestino;
    this.pedidoAceite.forca = pedidoIntroducaoPendente.forca;
    this.pedidoAceite.tags = pedidoIntroducaoPendente.tags;
    this.pedidoAceite.descricaoIntroducao = this.pedidoIntroAceiteForm.value.descricao;

    this.pedidoService.aceitarPedidoIntroducao(this.pedidoAceite).subscribe((result) => {
      if (result != null) {
        this.mostrarNotificacao('Pedido Aceite com Sucesso!', false);
        alert();

        this.removerDaLista(pedidoIntroducaoPendente);
      } else
        this.mostrarNotificacao('Pedido de introdução não aceite!', true);
    });
  }

  rejeitarPedidoIntroducao(pedidoIntroducaoPendente: IPedidoIntroDTO) {
    this.pedidoService.rejeitarPedidoIntroducao(pedidoIntroducaoPendente).subscribe((result) => {
      if (result != null) {
        this.mostrarNotificacao('Pedido Rejeitado com Sucesso!', true);
        this.removerDaLista(pedidoIntroducaoPendente);
      } else {
        this.mostrarNotificacao("Pedido não Rejeitado!", true)
      }
    });
  }

  colocarTextoPedidoIntroducao() {
    this.pedidoIntroducaoAceite = true;
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
