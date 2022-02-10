import {Component, NgZone, OnInit} from '@angular/core';
import {ILoginDTO} from "../dto/login-dto/ILoginDTO";
import {LoginService} from "../services/login-service/login.service";
import {Router} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  sessaoUtilizador: ILoginDTO = {email: '', password: ''};
  hide = true;

  constructor(private ngZone: NgZone,
              private router: Router,
              public loginService: LoginService,
              private notification: MatSnackBar) {
  }

  ngOnInit(): void {
    this.loginService.logout();
  }

  loginForm = new FormGroup({
    email: new FormControl("", Validators.required),
    password: new FormControl("", Validators.required)
  });


  logar() {
    const values = this.loginForm.value;
    this.loginService.getAuth(values.email, values.password).subscribe(result => {
      if (result) {
        this.mostrarNotificacao("Bem-Vindo(a) "+values.email,false);
        this.router.navigate(['/inicio']); //manda para a pagina inicial
      } else {
        this.mostrarNotificacao("Email ou password incorretos!", true);
      }
    })
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', { duration: 4000, panelClass: [snackbarColor] });
  }

  redirectParaRegisto() {
    this.router.navigate(['/registo']);
  }

}
