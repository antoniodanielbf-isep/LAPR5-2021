import {Component, NgZone, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {UtilizadorService} from "../../services/utilizador-service/utilizador.service";
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import {LoginService} from "../../services/login-service/login.service";
import IUtilizadorDTO from "../../dto/utilizador-dto/IUtilizadorDTO";
import IAlterarHumorDTO from "../../dto/utilizador-dto/IAlterarHumorDTO";
import {MatSnackBar} from "@angular/material/snack-bar";



@Component({
  selector: 'app-alterar-humor-utilizador',
  templateUrl: './alterar-humor-utilizador.component.html',
  styleUrls: ['./alterar-humor-utilizador.component.css']
})

export class AlterarHumorUtilizadorComponent implements OnInit {

  //@ts-ignore
  userInfo: IAuthDTO | null;

  utilizador: Promise<IUtilizadorDTO | undefined> = this.usrService.getUtilizador();

  constructor(public fb: FormBuilder,
              private ngZone: NgZone,
              private router: Router,
              private loginService: LoginService,
              public usrService: UtilizadorService,
              private notification: MatSnackBar) {
  }



  ngOnInit(): void {
    this.userInfo = this.loginService.userInfo;
  }



  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
