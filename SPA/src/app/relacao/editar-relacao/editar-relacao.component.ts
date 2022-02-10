import {Component, NgZone, OnInit} from '@angular/core';

import {RelacaoService} from "../../services/editar-relacao-service/relacao.service";
import {Router} from "@angular/router";
import IRelacaoDTO from "../../dto/relacao-dto/IRelacaoDTO";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import IRelacaoCriacaoDTO from "../../dto/relacao-dto/IRelacaoCriacaoDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-editar-relacao',
  templateUrl: './editar-relacao.component.html',
  styleUrls: ['./editar-relacao.component.css']
})
export class EditarRelacaoComponent implements OnInit {

  listaRelacoes: IRelacaoDTO[] = [];


  constructor(public relacaoService: RelacaoService,
              private ngZone: NgZone, private router: Router,
              private notification: MatSnackBar
  ) {

  }

    relacaoDto:IRelacaoCriacaoDTO = {
      forca: 0,
      tags: '',
    }

  editarRelacaoForm = new FormGroup({
    relacaoId: new FormControl('',Validators.required),
    forcaLigacaoOrigDest: new FormControl('', Validators.required),
    tagsRelacaoAB: new FormControl('', Validators.required)
  });

  ngOnInit(): void {
    this.relacaoService.getRelacoesUser().subscribe((listaRel) => {
      this.listaRelacoes = listaRel;
    });
  }

  irParaInicio() {
    this.router.navigate(['/inicio']);
  }

  submitForm(relacaoId: string) {
    this.relacaoDto.forca=this.editarRelacaoForm.value.forcaLigacaoOrigDest;
    this.relacaoDto.tags=this.editarRelacaoForm.value.tagsRelacaoAB;
    this.relacaoService.updateRelacao(this.relacaoDto,relacaoId).subscribe(res => {
      if (res != null) {
        this.router.navigate(['/editarRelacao']);
      }
    });
  }
  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
