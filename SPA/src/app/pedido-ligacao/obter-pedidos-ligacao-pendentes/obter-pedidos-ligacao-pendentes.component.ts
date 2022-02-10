import {Component, OnInit} from '@angular/core';
import {UtilizadorService} from "../../services/utilizador-service/utilizador.service";
import {PedidoLigacaoService} from "../../services/pedido-ligacao-service/pedido-ligacao.service";
import IPedidoLigacaoTodaDTO from "../../dto/pedido-ligacao-dto/IPedidoLigacaoTodaDTO";
import {Router} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import IRelacaoCriacaoDTO from "../../dto/relacao-dto/IRelacaoCriacaoDTO";
import {IPedidoLigacaoAceiteDTO} from "../../dto/pedido-ligacao-dto/IPedidoLigacaoAceiteDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-obter-pedidos-ligacao-pendentes',
  templateUrl: './obter-pedidos-ligacao-pendentes.component.html',
  styleUrls: ['./obter-pedidos-ligacao-pendentes.component.css']
})
export class ObterPedidosLigacaoPendentesComponent implements OnInit {
  constructor(private utilizadorService: UtilizadorService,
              private pedLService: PedidoLigacaoService,
              private router: Router,
              private notification: MatSnackBar
  ) {
  }

  pedidosLigacaoPendentes: IPedidoLigacaoTodaDTO[] = [];
  pedidoLigacaoAceite: boolean = false;


  ngOnInit(): void {

    const that = this;

    this.pedLService.getPedidosLigacaoPendentes().then(function (v) {
      console.log(v);
      if (v != null) {
        that.pedidosLigacaoPendentes = v;
      }
    }).catch(function (x) {
      alert('NENHUM PEDIDO DE LIGAÇÃO PENDENTE ENCONTRADO!');
    });

    //@ts-ignore
    if (this.pedidosLigacaoPendentes?.length > 0) {
      this.mostrarNotificacao('Encontrados ' + this.pedidosLigacaoPendentes?.length + " Pedidos de Ligação Pendentes!",false);
    }
  }

  relacaoForm = new FormGroup({
    forcaLigacao: new FormControl('', Validators.required),
    tagsRelacao: new FormControl('', Validators.required)
  });

  relacao: IRelacaoCriacaoDTO = {forca: 0, tags: ''};
  pedidoLigacaoAceiteDTO: IPedidoLigacaoAceiteDTO = {forca: 0, tagsL: ''};

  voltarAoInicio() {
    this.router.navigate(['/inicio']);
  }

  //remove o item da lista -> splice remove a partir do item passado por parâmetro 1 item, que será o próprio item
  removerDaLista(pedidoLigacaoPendente: IPedidoLigacaoTodaDTO) {
    this.pedidosLigacaoPendentes.splice(this.pedidosLigacaoPendentes.indexOf(pedidoLigacaoPendente), 1);
  }

  rejeitarPedidoLigacao(pedidoLigacaoPendente: IPedidoLigacaoTodaDTO) {
    this.pedLService.rejeitarLigacao(pedidoLigacaoPendente).subscribe((result) => {
      if (result != null) {
        this.mostrarNotificacao('Introdução Rejeitada com Sucesso!',false)
        alert();
        this.removerDaLista(pedidoLigacaoPendente);
      } else
        this.mostrarNotificacao('Pedido não Rejeitado!',true)
    });
  }

  colocarTextoPedidoLigacao() {
    this.pedidoLigacaoAceite = true;
  }

  submitFormRel(ligacaoPendente: IPedidoLigacaoTodaDTO) {
    this.relacao.forca = this.relacaoForm.value.forcaLigacao;
    this.relacao.tags = this.relacaoForm.value.tagsRelacao;
    this.aceitarPedidoLigacao(ligacaoPendente);
  }

  aceitarPedidoLigacao(ligacaoPendente: IPedidoLigacaoTodaDTO) {
    //aceitamos quem originou o pedido de ligacao
    this.pedidoLigacaoAceiteDTO.forca = this.relacao.forca;
    this.pedidoLigacaoAceiteDTO.tagsL = this.relacao.tags;

    //o backend invalida qualquer relacao cuja forca nao esteja entre 1 e 100
    this.pedLService.aceitarLigacao(ligacaoPendente.id, this.pedidoLigacaoAceiteDTO).subscribe((result) => {
      if (result != null) {
        this.mostrarNotificacao('Introdução Aceite com Sucesso! Uma nova Relação foi Estabelecida!',false)
        this.removerDaLista(ligacaoPendente);
        this.router.navigate(['/inicio']);
      } else
        this.mostrarNotificacao('Pedido não Aceite!',true)
    });
  }
  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
