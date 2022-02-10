import {Component, NgZone, OnInit} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {Router} from "@angular/router";
import {UtilizadorService} from "../services/utilizador-service/utilizador.service";
import {InicioService} from "../services/inicio-service/inicio.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent implements OnInit {


  constructor(public fb: FormBuilder,
              private ngZone: NgZone,
              private router: Router,
              private service: InicioService,
              private usrService: UtilizadorService,
              private notification: MatSnackBar) {
  }
  

  ngOnInit(): void {

  }

  logout() {
    this.service.logout();
  }

  visualizarRede() {
    this.router.navigate(['/rede-social'])
  }

  visualizarTabelas() {
    this.router.navigate(['/redeSocialPerspetiva'])
  }

  editarPerfil() {
    this.router.navigate(['/editarInfo']);
  }

  pesquisarUser() {
    this.router.navigate(['/pesquisarUser']);
  }

  editarRelacao() {
    this.router.navigate(['/editarRelacao']);
  }

  acrgIntro() {
    this.router.navigate(['/aceitarRejeitarIntro']);
  }

  criarIntro() {
    this.router.navigate(['/pedirIntroducao']);
  }

  escolherUtilizadoresObjetivoSugeridos() {
    this.router.navigate(['/escolherUtilizadoresObjetivo']);
  }

  verPedidosLigacaoPendentes() {
    this.router.navigate(['/obterPedidosLigacaoPendentes']);
  }

  apDesPedIntro() {
    this.router.navigate(['/aprovarDesaprovarPedIntro'])
  }


  verTagsCloud() {
    this.router.navigate(['/tagsCloud']);
  }

  criarPost() {
    this.router.navigate(['/criar-post']);
  }

  consultarAmigosComuns() {
    this.router.navigate(['/amigosComuns']);
  }

  verSugAmizade() {
    this.router.navigate(['/sugestoesAmigos']);
  }

  verFeed() {
    this.router.navigate(['/verFeed']);
  }

  consultarLeaderbords() {
    this.router.navigate(['/leaderboards']);
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
