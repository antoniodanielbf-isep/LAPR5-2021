import {Component, NgZone, OnInit} from '@angular/core';
import IIntroDTO from "../dto/intro-dto/IIntroDTO";
import {Router} from "@angular/router";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {IntroducaoService} from "../services/intro-service/introducao.service";
import IPedidoIntroDTO from "../dto/pedido-intro-dto/IPedidoIntroDTO";
import IRelacaoCriacaoDTO from "../dto/relacao-dto/IRelacaoCriacaoDTO";
import {RelacaoService} from "../services/editar-relacao-service/relacao.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-aceitar-rejeitar-introducao',
  templateUrl: './aceitar-rejeitar-introducao.component.html',
  styleUrls: ['./aceitar-rejeitar-introducao.component.css']
})
export class AceitarRejeitarIntroducaoComponent implements OnInit {
  listaIntroducoesPendentes: IIntroDTO[] = [];

  constructor(public fb: FormBuilder,
              private ngZone: NgZone,
              private router: Router,
              public introService: IntroducaoService,
              public relacaoService: RelacaoService,
              private notification: MatSnackBar) {
  }

  ngOnInit(): void {

    this.introService.getIntroducoesPendentesUtilizador().subscribe((listaIntroducoes) => {
      this.listaIntroducoesPendentes = listaIntroducoes;
    });

  }

  pedidoIntro: IPedidoIntroDTO = {
    id: '',
    descricao: '',
    emailOrigem: '',
    emailIntermedio: '',
    emailDestino: '',
    estado: 0,
    forca: 0,
    tags: ''
  };

  relacaoForm = new FormGroup({
    forcaLigacao: new FormControl('', Validators.required),
    tagsRelacao: new FormControl('', Validators.required)
  });

  relacao: IRelacaoCriacaoDTO = {forca: 0, tags: ''};
  introducaoAceite: boolean = false;

  aceitarIntroducao(introducaoPendente: IIntroDTO) {

    //o backend invalida qualquer relacao cuja forca nao esteja entre 1 e 100
    this.introService.aceitarIntroducao(introducaoPendente, this.relacao).subscribe((result) => {
      if (result != null) {
        this.mostrarNotificacao("Introdução Aceite com Sucesso! Uma nova Relação foi Estabelecida!", false);
        this.removerDaLista(introducaoPendente);
        this.router.navigate(['/inicio']);
      } else
        this.mostrarNotificacao("Introdução não Aceite!", true);

    });

  }

  rejeitarIntroducao(introducaoPendente: IIntroDTO) {
    this.introService.rejeitarIntroducao(introducaoPendente).subscribe((result) => {
      if (result != null) {
        this.mostrarNotificacao("Introdução Rejeitada com Sucesso!", false);
        alert('');
        this.removerDaLista(introducaoPendente);
      } else
        this.mostrarNotificacao("Pedido não Rejeitado!", true);
    });
  }

  //remove o item da lista -> splice remove a partir do item passado por parâmetro 1 item, que será o próprio item
  removerDaLista(introducaoPendente: IIntroDTO) {
    this.listaIntroducoesPendentes.splice(this.listaIntroducoesPendentes.indexOf(introducaoPendente), 1);
  }

  irParaInicio() {
    this.router.navigate(['/inicio']);
  }

  submitFormRel(introducaoPendente: IIntroDTO) {
    this.relacao.forca = this.relacaoForm.value.forcaLigacao;
    this.relacao.tags = this.relacaoForm.value.tagsRelacao;
    this.aceitarIntroducao(introducaoPendente);
  }

  aceitarColocarRelacao() {
    this.introducaoAceite = true;
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
