import {Component, NgZone, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {RedeSocialService} from "../../services/rede-social-service/RedeSocialService";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-amigos-comuns-entre-dois-users',
  templateUrl: './amigos-comuns-entre-dois-users.component.html',
  styleUrls: ['./amigos-comuns-entre-dois-users.component.css']
})
export class AmigosComunsEntreDoisUsersComponent implements OnInit {
  amigosComunsEncontrados: boolean = false;

  AmigosComunsForm = new FormGroup({
    email: new FormControl('', Validators.required),
  });

  listaAmigosComuns: Array<string> = [''];

  constructor(public redeSocialService: RedeSocialService,
              private ngZone: NgZone, private router: Router, private notification: MatSnackBar) {
  }

  ngOnInit(): void {
  }

  buscarAmigosComuns() {
    this.redeSocialService.getAmigosComuns(this.AmigosComunsForm.value.email).subscribe(res => {
      if (res == null) {
        this.mostrarNotificacao("IMPOSSÍVEL CARREGAR AMIGOS COMUNS!", true);
      }
      this.listaAmigosComuns = res;
      this.amigosComunsEncontrados = true;
      this.mostrarNotificacao("Carregando amigos comuns!", false);
    });
  }

  irParaInicio() {
    this.router.navigate(['/inicio']);
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
