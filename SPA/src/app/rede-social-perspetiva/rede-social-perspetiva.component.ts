import {Component, NgZone, OnInit} from '@angular/core';
import {Router} from "@angular/router";

import {RedeSocialService} from "../services/rede-social-service/RedeSocialService";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import IRedeSocialGetDTO from "../dto/rede-social-dto/IRedeSocialGetDTO";
import {ICaminhoDTO} from "../dto/rede-social-dto/ICaminhoDTO";
import {ITamanhoDTO} from "../dto/rede-social-dto/ITamanhoDTO";
import {ICaminhoEForcaLigacaoDTO} from "../dto/rede-social-dto/ICaminhoEForcaLigacaoDTO"
import IRelacaoDTO from "../dto/relacao-dto/IRelacaoDTO";
import {ITamanhoRedeSocialTotalDTO} from "../dto/rede-social-dto/ITamanhoRedeSocialTotalDTO";
import {UtilizadorService} from "../services/utilizador-service/utilizador.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ICaminhoFLigacaoRelacaoDTO} from "../dto/rede-social-dto/ICaminhoLigRelDTO";

interface Nivel {
  nivel: string;
  number: string;
}

@Component({
  selector: 'app-rede-social-perspetiva',
  templateUrl: './rede-social-perspetiva.component.html',
  styleUrls: ['./rede-social-perspetiva.component.css']
})
export class RedeSocialPerspetivaComponent implements OnInit {

  redeSocialPerspetiva: Array<Array<IRelacaoDTO>> = [];
  checkbox: boolean = false;

  constructor(public redeSocialService: RedeSocialService, public utilizadorService: UtilizadorService,
              private ngZone: NgZone, private router: Router, public notification: MatSnackBar) {
  }


  totalUtilizadoresSistema: ITamanhoRedeSocialTotalDTO = {tamanhoRedeSocialCompleto: 0};

  ngOnInit(): void {
    this.redeSocialService.getTotalUtilizadores().subscribe(res => {
      if (res != null) {
        this.totalUtilizadoresSistema.tamanhoRedeSocialCompleto = res.tamanhoRedeSocialCompleto;
      }
    })
  }

  niveis: Nivel[] = [
    {nivel: '2', number: '2'},
    {nivel: '3', number: '3'},
  ];

  opcaoEscolhida: IRedeSocialGetDTO = {
    nivel: 0,
    utilizadorDestino: '',
    valorMinimo: 0,
  }

  editarRede = new FormGroup({
    nivelEditado: new FormControl(''),
    utilizadorDestino: new FormControl('', Validators.required),
    valorMinimo: new FormControl('', Validators.required),
    forcaLigacao: new FormControl('', Validators.required),
    forcaRelacao: new FormControl('', Validators.required),
    valorMaximo: new FormControl('', Validators.required)
  });
  valorMaximoIntroduzido: number = 0;

  caminhoEncontrado: ICaminhoDTO = {
    caminho: [''], valor: 0
  }

  forcaEncontrada: ICaminhoEForcaLigacaoDTO = {
    caminho: [''],
    valor: 0,
    forcasLigacaoDestinoOrigem: [],
    forcasLigacaoOrigemDestino: []
  }

  forcaRelacaoEncontrada: ICaminhoFLigacaoRelacaoDTO = {
    caminho: [''],
    valor: 0,
    forcasLigacaoDestinoOrigem: [],
    forcasLigacaoOrigemDestino: [],
    forcasRelacaoDestinoOrigem: [],
    forcasRelacaoOrigemDestino: []
  }

  caminhoSeguro: boolean = false;
  redeAtiva: boolean = false;
  caminhoMaisCurtoEncontrado: boolean = false;
  caminhoMaisForteEncontrado: boolean = false;
  caminhoMaisSeguroEncontrado: boolean = false;
  caminhoComForcaEncontrado: boolean = false;
  caminhoComForcaRelEncontrado: boolean = false;


  //ou mudamos os users da rede pelos users de cada caminho e aí invocamos o highlight ou invocamos o highlight diretamente
  consultarCaminhoMaisCurto() {
    if (!this.checkbox) {
      //ocultar outros caminhos
      this.caminhoMaisSeguroEncontrado = false;
      this.caminhoMaisForteEncontrado = false;
      this.editarRede.value.nivelEditado=0;


      this.opcaoEscolhida.utilizadorDestino = this.editarRede.value.utilizadorDestino;
      this.redeSocialService.getCaminhoMaisCurto(this.opcaoEscolhida.utilizadorDestino).subscribe((caminhoMaisCurto) => {
        if (caminhoMaisCurto != null) {
          this.caminhoEncontrado = caminhoMaisCurto;
          this.caminhoMaisCurtoEncontrado = true;
        }
        if (caminhoMaisCurto == null) {
          this.mostrarNotificacao('CAMINHO VAZIO!', true);
        }
      });
    }
  }

  consultarCaminhoMaisForte() {
    this.caminhoMaisSeguroEncontrado = false;
    this.caminhoMaisCurtoEncontrado = false;
    this.editarRede.value.nivelEditado=0;

    this.opcaoEscolhida.utilizadorDestino = this.editarRede.value.utilizadorDestino;
    this.redeSocialService.getCaminhoMaisForte(this.opcaoEscolhida.utilizadorDestino).subscribe((caminhoMaisForte) => {
      if (caminhoMaisForte != null) {
        this.caminhoEncontrado = caminhoMaisForte;
        this.caminhoMaisForteEncontrado = true;
      }
      if (caminhoMaisForte == null) {
        this.mostrarNotificacao('CAMINHO VAZIO!', true);
      }
    })
  }

  consultarCaminhoMaisSeguro() {
    //ocultar outros caminhos e a rede
    this.caminhoMaisCurtoEncontrado = false;
    this.caminhoMaisForteEncontrado = false;
    this.editarRede.value.nivelEditado=0;

    this.opcaoEscolhida.utilizadorDestino = this.editarRede.value.utilizadorDestino;
    this.opcaoEscolhida.valorMinimo = this.editarRede.value.valorMinimo;
    this.redeSocialService.getCaminhoMaisSeguro(this.opcaoEscolhida.utilizadorDestino, this.opcaoEscolhida.valorMinimo)
      .subscribe((caminhoMaisSeguro) => {
        if (caminhoMaisSeguro != null) {
          this.caminhoEncontrado = caminhoMaisSeguro;
          this.caminhoMaisSeguroEncontrado = true;
        }
        if (caminhoMaisSeguro == null) {
          this.mostrarNotificacao('CAMINHO VAZIO!', true);
        }
      });
    this.caminhoSeguro = false;
  }

  mostrarFormValorMinimo() {
    if (this.caminhoSeguro) {
      this.caminhoSeguro = false;
    } else {
      this.caminhoSeguro = true;
    }
  }

  pesquisarForcaLigacao() {
    this.caminhoMaisCurtoEncontrado = false;
    this.caminhoMaisForteEncontrado = false;
    this.caminhoMaisSeguroEncontrado = false;
    this.editarRede.value.nivelEditado=0;

    this.valorMaximoIntroduzido = this.editarRede.value.valorMaximo;
    this.opcaoEscolhida.utilizadorDestino = this.editarRede.value.utilizadorDestino;
    this.redeSocialService.getCaminhoByForca(this.editarRede.value.utilizadorDestino, this.editarRede.value.valorMaximo)
      .subscribe((caminhoForcaEncontrado) => {
        if (caminhoForcaEncontrado != null) {
          this.forcaEncontrada = caminhoForcaEncontrado;
          this.caminhoComForcaEncontrado = true;
        }
        if (caminhoForcaEncontrado == null) {
          this.mostrarNotificacao('CAMINHO VAZIO!', true);
        }
      });
  }

  pesquisarForcaLigacaoRelacao() {
    this.caminhoMaisCurtoEncontrado = false;
    this.caminhoMaisForteEncontrado = false;
    this.caminhoMaisSeguroEncontrado = false;
    this.editarRede.value.nivelEditado=0;

    this.valorMaximoIntroduzido = this.editarRede.value.valorMaximo;
    this.opcaoEscolhida.utilizadorDestino = this.editarRede.value.utilizadorDestino;
    this.redeSocialService.getCaminhoByForcaRel(this.opcaoEscolhida.utilizadorDestino, this.editarRede.value.valorMaximo).subscribe((caminhoForcaRelEncontrado) => {
      if (caminhoForcaRelEncontrado != null) {
        this.forcaRelacaoEncontrada = caminhoForcaRelEncontrado;
        this.caminhoComForcaRelEncontrado = true;
      }
      if (caminhoForcaRelEncontrado == null) {
        this.mostrarNotificacao('CAMINHO VAZIO!', true);
      }
    });
  }

  fortalezaRedeCalculada: boolean = false;
  fortalezaRede: number = 0;

  consultarFortalezaDaRede() {
    this.caminhoMaisCurtoEncontrado = false;
    this.caminhoMaisForteEncontrado = false;
    this.caminhoMaisSeguroEncontrado = false;
    this.editarRede.value.nivelEditado=0;

    this.redeSocialService.getFortalezaRede().subscribe((fortalezaRedeDevolvida) => {
      if (fortalezaRedeDevolvida != null) {
        this.fortalezaRede = fortalezaRedeDevolvida;
        this.fortalezaRedeCalculada = true;
      }
      if (fortalezaRedeDevolvida == null) {
        this.mostrarNotificacao('FORTALEZA DE REDE NULA! IMPOSSÍVEL DE CALCULAR! TENHA PELO MENOS 1 RELAÇÃO COM ALGUÉM!', true);
      }
    });
  }

  tamanhoDaRede: ITamanhoDTO = {users: [''], tamanho: 0}
  tamanhoRedeCalculado: boolean = false;
  tamanhoRedeForm = new FormGroup({
    nivelEditado: new FormControl('', Validators.required)
  });

  consultarTamanhoDaRede() {
    this.caminhoMaisCurtoEncontrado = false;
    this.caminhoMaisForteEncontrado = false;
    this.caminhoMaisSeguroEncontrado = false;
    this.editarRede.value.nivelEditado=0;

    this.redeSocialService.getTamanhoRede(this.tamanhoRedeForm.value.nivelEditado).subscribe((tamanhoRedeDevolvido) => {
      if (tamanhoRedeDevolvido != null) {
        this.tamanhoDaRede = tamanhoRedeDevolvido;
        this.tamanhoRedeCalculado = true;
      }
      if (tamanhoRedeDevolvido == null) {
        this.mostrarNotificacao('TAMANHO DE REDE NULO! IMPOSSÍVEL DE CALCULAR! TENHA PELO MENOS 1 RELAÇÃO COM ALGUÉM!', true);
      }
    });
  }

  irParaInicio() {
    this.router.navigate(['/inicio']);
  }

  //-----------------------GRAFO--------------------------------------//


  consultarRede() {
    this.caminhoMaisCurtoEncontrado = false;
    this.caminhoMaisForteEncontrado = false;
    this.caminhoMaisSeguroEncontrado = false;

    this.redeSocialService.getRedeSocialPerspetiva(this.editarRede.value.nivelEditado).subscribe(res => {
      if (res != null) {
        this.redeSocialPerspetiva = res;
        this.redeAtiva = true;
      } else this.mostrarNotificacao("REDE SOCIAL CARREGADA SEM SUCESSO", true);
    })
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }

  caminhoMaisCurtoEmocionalEncontrado: boolean = false;
  caminhoMaisForteEmocionalEncontrado: boolean = false;
  caminhoMaisSeguroEmocionalEncontrado: boolean = false;
  caminhoSeguroEmocional: boolean = false;

  consultarCaminhoMaisCurtoEmocional() {
    this.opcaoEscolhida.utilizadorDestino = this.editarRede.value.utilizadorDestino;
    this.valorMaximoIntroduzido = this.editarRede.value.valorMaximo;
    this.redeSocialService.getCaminhoMaisCurtoEmocional(this.opcaoEscolhida.utilizadorDestino, this.valorMaximoIntroduzido).subscribe((caminhoMaisCurto) => {
      if (caminhoMaisCurto != null) {
        this.caminhoEncontrado = caminhoMaisCurto;
        this.caminhoMaisCurtoEmocionalEncontrado = true;
      }
      if (caminhoMaisCurto == null) {
        this.mostrarNotificacao('CAMINHO VAZIO!', true);
      }
    });
  }

  consultarCaminhoMaisForteEmocional() {
    this.opcaoEscolhida.utilizadorDestino = this.editarRede.value.utilizadorDestino;
    this.valorMaximoIntroduzido = this.editarRede.value.valorMaximo;
    this.redeSocialService.getCaminhoMaisForteEmocional(this.opcaoEscolhida.utilizadorDestino, this.valorMaximoIntroduzido)
      .subscribe((caminhoMaisForte) => {
        if (caminhoMaisForte != null) {
          this.caminhoEncontrado = caminhoMaisForte;
          this.caminhoMaisForteEmocionalEncontrado = true;
        }
        if (caminhoMaisForte == null) {
          this.mostrarNotificacao('CAMINHO VAZIO!', true);
        }
      })
  }

  consultarCaminhoMaisSeguroEmocional() {
    this.opcaoEscolhida.utilizadorDestino = this.editarRede.value.utilizadorDestino;
    this.opcaoEscolhida.valorMinimo = this.editarRede.value.valorMinimo;
    this.redeSocialService.getCaminhoMaisSeguroEmocional(this.opcaoEscolhida.utilizadorDestino, this.opcaoEscolhida.valorMinimo,
      this.valorMaximoIntroduzido)
      .subscribe((caminhoMaisSeguro) => {
        if (caminhoMaisSeguro != null) {
          this.caminhoEncontrado = caminhoMaisSeguro;
          this.caminhoMaisSeguroEmocionalEncontrado = true;
        }
        if (caminhoMaisSeguro == null) {
          this.mostrarNotificacao('CAMINHO VAZIO!', true);
        }
      });
    this.caminhoSeguroEmocional = false;
  }

  toggle() {
    this.checkbox = !this.checkbox;
  }

  verCaminhoMaisCurtoEmocional() {
    if (this.caminhoSeguroEmocional) {
      this.caminhoSeguroEmocional = false;
    } else {
      this.caminhoSeguroEmocional = true;
    }
  }
}
