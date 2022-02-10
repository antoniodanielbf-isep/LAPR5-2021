import {Component, OnInit, NgZone} from '@angular/core';
import {UtilizadorService} from '../../services/utilizador-service/utilizador.service';
import {Router} from '@angular/router';
import {FormBuilder, FormGroup, Validators, FormControl} from '@angular/forms'
import {urlBaseMDR} from '../../config';
import {IUtilizadorMDPDTO} from "../../dto/utilizador-dto/IUtilizadorMDPDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

interface EstadoHumor {
  estado: string;
  number: string;
}

@Component({
  selector: 'app-create-utilizador',
  templateUrl: './create-utilizador.component.html',
  styleUrls: ['./create-utilizador.component.css']
})
export class CreateUtilizadorComponent implements OnInit {

  constructor(public fb: FormBuilder,
              private ngZone: NgZone,
              private router: Router,
              public usrService: UtilizadorService,
              private notification: MatSnackBar) {
  }

  urlBase = urlBaseMDR + "Utilizador";
  usrArray: any = [];
  utilizadorForm = new FormGroup({
    nomeUtilizador: new FormControl('', Validators.required),
    breveDescricaoUtilizador: new FormControl('', Validators.required),
    email: new FormControl('', Validators.required),
    numeroDeTelefoneUtilizador: new FormControl('', Validators.required),
    dataDeNascimentoUtilizador: new FormControl('', Validators.required),
    perfilFacebookUtilizador: new FormControl('', Validators.required),
    perfilLinkedinUtilizador: new FormControl('', Validators.required),
    estadoEmocionalUtilizador: new FormControl('', Validators.required),
    tagsUtilizador: new FormControl('', Validators.required),
    urlImagem: new FormControl('', Validators.required),
    cidadePais: new FormControl('', Validators.required),
    dataModificacao: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  estados: EstadoHumor[] = [
    {estado: 'Alegria', number: '0'},
    {estado: 'Angustia', number: '1'},
    {estado: 'Esperanca', number: '2'},
    {estado: 'Medo', number: '3'},
    {estado: 'Alivio', number: '4'},
    {estado: 'Dececao', number: '5'},
    {estado: 'Orgulho', number: '6'},
    {estado: 'Remorsos', number: '7'},
    {estado: 'Gratidao', number: '8'},
    {estado: 'Raiva', number: '9'}
  ];

  utilizador:IUtilizadorMDPDTO={
    email:"email",
    nome:"nome"
  }

  checkado:boolean=false;
  hide = true;

  ngOnInit() {
  }

  submitForm() {

    if(this.checkado) {
      this.checkado=false;

      this.utilizador.nome=this.utilizadorForm.value.nomeUtilizador;
      this.utilizador.email=this.utilizadorForm.value.email;

      this.usrService.createUtilizador(this.utilizadorForm.value).subscribe(res => {
        if (res != null) {

          this.usrService.createUtilizadorMDP(this.utilizador).subscribe(res=>{
            if(res != null){
              this.router.navigate(['/escolherUtilizadoresObjetivo']);
            }
          });
        }
      });
    }
    else{
      this.mostrarNotificacao("Tem de declarar o seu consentimento na checkbox!", true)
    }
  }

  redirectParaTermos() {
    this.router.navigate(['/termos']);
  }

  checkar() {
    this.checkado=true;
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
