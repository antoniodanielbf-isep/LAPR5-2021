import {Component, NgZone, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {UtilizadorService} from "../../services/utilizador-service/utilizador.service";
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import {LoginService} from "../../services/login-service/login.service";
import IUtilizadorDTO from "../../dto/utilizador-dto/IUtilizadorDTO";
import {MatSnackBar} from "@angular/material/snack-bar";
import {IUtilizadorEraseDTOMap} from "../../mappers/utilizador-mapper/IUtilizadorEraseDTOMap";
import {IUtilizadorEraseDTO} from "../../dto/utilizador-dto/IUtilizadorEraseDTO";
import {InicioService} from "../../services/inicio-service/inicio.service";
import IAlterarHumorDTO from "../../dto/utilizador-dto/IAlterarHumorDTO";

interface EstadoHumor {
  estado: string;
  number: string;
}

@Component({
  selector: 'app-change-utilizador-info',
  templateUrl: './change-utilizador-info.component.html',
  styleUrls: ['./change-utilizador-info.component.css']
})

export class ChangeUtilizadorInfoComponent implements OnInit {

  aparecerOpcoes: boolean = false;
  utilizadorApagado: IUtilizadorEraseDTO = {old: '', novo: ''}
  userInfo: IAuthDTO = {email: ''};

  utilizador: IUtilizadorDTO = {
    nomeUtilizador: '',
    breveDescricaoUtilizador: '',
    email: '',
    numeroDeTelefoneUtilizador: '',
    dataDeNascimentoUtilizador: '',
    perfilFacebookUtilizador: '',
    perfilLinkedinUtilizador: '',
    estadoEmocionalUtilizador: 0,
    tagsUtilizador: '',
    urlImagem: '',
    cidadePaisResidencia: '',
    dataModificacaoEstado: '',
    passwordU: ''
  };


  constructor(public fb: FormBuilder,
              private ngZone: NgZone,
              private router: Router,
              private loginService: LoginService,
              private service: InicioService,
              public usrService: UtilizadorService,
              private notification: MatSnackBar) {
  }

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
    {estado: 'Raiva', number: '9'},
    {estado: 'Gosto', number: '10'},
    {estado: 'NaoGosto', number: '11'},
  ];


  opcaoEscolhida: IAlterarHumorDTO = {
    estadoEmocionalUtilizador: 0,
  }

  editarHumorForm = new FormGroup({
    estado: new FormControl('')
  });

  ngOnInit(): void {
    this.userInfo = this.usrService.userInfo;
    // @ts-ignore
    //this.utilizador = this.usrService.getUtilizador();

    const that = this;

    this.usrService.getUtilizador().then(function (v) {
      console.log(v);
      that.utilizador = v;
    })

    //this.utilizador.nomeUtilizador;
  }

  irParaInicio() {
    this.router.navigate(['/inicio']);
  }

  //passar os dados por parâmetro para dar update

  editarForm = new FormGroup({
    nomeUtilizador: new FormControl(''),
    breveDescricaoUtilizador: new FormControl('', Validators.max(100)),
    perfilFacebookUtilizador: new FormControl(''),
    perfilLinkedinUtilizador: new FormControl(''),
    tagsUtilizador: new FormControl(''),
    urlImagem: new FormControl(''),
    cidadePais: new FormControl(''),
    password: new FormControl('')
  });


  submitForm() {
    this.usrService.updateUtilizador(this.editarForm.value).subscribe(res => {
        if (res != null) {
          this.mostrarNotificacao("Informações alteradas com sucesso!", false);
          this.router.navigate(['/inicio']);
        }
      }
    );

  }

  aparecer() {
    if (!this.aparecerOpcoes) {
      this.aparecerOpcoes = true;
    } else {
      this.aparecerOpcoes = false;
    }
  }

  apagarDados() {
    this.usrService.apagarUtilizadorMDR().subscribe(result => {
      if (result != null) {

        this.utilizadorApagado = IUtilizadorEraseDTOMap.toDTO(this.usrService.userInfo.email, result.email);
        this.usrService.apagarUtilizadorMDP(this.utilizadorApagado).subscribe(result => {
          if (result != null) {
            this.mostrarNotificacao("DADOS APAGADOS COM SUCESSO! ADEUS! ATÉ À PRÓXIMA", false);
            this.service.logout();
            this.router.navigate(['']);
          }
        })
      } else {
        this.mostrarNotificacao("DADOS APAGADOS SEM SUCESSO!", false);
      }
    })
  }

  alterarHumor() {
    this.opcaoEscolhida.estadoEmocionalUtilizador = this.editarHumorForm.value.estado;
    this.mostrarNotificacao("" + this.opcaoEscolhida.estadoEmocionalUtilizador, true);
    alert(this.opcaoEscolhida.estadoEmocionalUtilizador);
    this.usrService.updateEstadoHumorUtilizador(this.opcaoEscolhida).subscribe(res => {
        if (res != null) {
          this.router.navigate(['/editarInfo']);
        }
      }
    );
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
