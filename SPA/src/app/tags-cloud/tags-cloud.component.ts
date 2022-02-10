import {Component, NgZone, OnInit} from '@angular/core';
import {RelacaoService} from "../services/editar-relacao-service/relacao.service";
import {Router} from "@angular/router";
import {IRelacaoTagsDTO} from "../dto/relacao-dto/IRelacaoTagsDTO";
import {IUtilizadorTagsDTO} from "../dto/utilizador-dto/IUtilizadorTagsDTO";
import {UtilizadorService} from "../services/utilizador-service/utilizador.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-tags-cloud',
  templateUrl: './tags-cloud.component.html',
  styleUrls: ['./tags-cloud.component.css']
})
export class TagsCloudComponent implements OnInit {

  constructor(public relacaoService: RelacaoService,
              public userService: UtilizadorService,
              private ngZone: NgZone, private router: Router,
              private notification: MatSnackBar) {
  }

  ngOnInit(): void {
  }

  tagsTodosUtilizadores: IUtilizadorTagsDTO = {tags: ''};
  tagsTodosUtilizadoresVistas: boolean = false;

  tagsUtilizador: IUtilizadorTagsDTO = {tags: ''};
  tagsUtilizadorVistas: boolean = false;

  tagsTodasRelacaoes: IRelacaoTagsDTO = {tags: ''};
  tagsTodasRelacoesVistas: boolean = false;

  tagsRelacaoesUtilizador: IRelacaoTagsDTO = {tags: ''};
  tagsRelacoesUtilizadorVistas: boolean = false;

  irParaInicio() {
    this.router.navigate(['/inicio']);
  }

  verTagCloudTodosUtilizadores() {
    this.userService.getTagsAllUsers().subscribe(res => {
      if (res != null) {
        this.tagsTodosUtilizadores = res;
        this.tagsTodosUtilizadoresVistas=true;
        alert("TagsTodosUtilizadores carregada COM sucesso!")
      } else alert("TagsTodosUtilizadores carregada SEM sucesso!")
    },
      error => {
        if (error.status == 404) {
          alert('Pedido Não Encontrado!');
        }
        if (error.status == 500) {
          alert('Erro do MDR!');
        }
    });
  }

  verTagCloudUtilizador() {
    this.userService.getTagsUser().subscribe(res => {
        if (res != null) {
          this.tagsUtilizador = res;
          this.tagsUtilizadorVistas=true;
          alert("TagsUtilizador carregada COM sucesso!")
        } else {
          alert("TagsUtilizador carregada SEM sucesso!")
        }
      },
      error => {
        if (error.status == 404) {
          alert('Pedido Não Encontrado!');
        }
        if (error.status == 500) {
          alert('Erro do MDR!');
        }
      }
    );
  }

  verTagCloudTodasRelacoes() {
    this.relacaoService.getTagsAllUsers().subscribe(res => {
      if (res != null) {
        this.tagsTodasRelacaoes = res;
        this.tagsTodasRelacoesVistas=true;
        alert("TagsTodasRelacoes carregada COM sucesso!")
      } else alert("TagsTodasRelacoes carregada SEM sucesso!")
    },
        error => {
      if (error.status == 404) {
        alert('Pedido Não Encontrado!');
      }
      if (error.status == 500) {
        alert('Erro do MDR!');
      }
    });
  }

  verTagCloudRelacoesUtilizador() {
    this.relacaoService.getTagsUser().subscribe(res => {
      if (res != null) {
        this.tagsRelacaoesUtilizador = res;
        this.tagsRelacoesUtilizadorVistas=true;
        alert("TagsRelacoesUtilizador carregada COM sucesso!")
      } else alert("TagsRelacoesUtilizador carregada SEM sucesso!")
    },
      error => {
        if (error.status == 404) {
          alert('Pedido Não Encontrado!');
        }
        if (error.status == 500) {
          alert('Erro do MDR!');
        }
      });
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
