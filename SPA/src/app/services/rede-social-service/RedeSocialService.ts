import {HttpClient, HttpHeaders} from "@angular/common/http";

import {Injectable} from "@angular/core";
import IRedeSocialPerspetivaDTO from "../../dto/rede-social-dto/IRedeSocialPerspetivaDTO";
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import {urlBaseMDR, urlBaseIA} from "../../config";
import {catchError, Observable, throwError} from "rxjs";
import IRedeSocialGetDTO from "../../dto/rede-social-dto/IRedeSocialGetDTO";
import {UtilizadorService} from "../utilizador-service/utilizador.service";
import {ICaminhoDTO} from "../../dto/rede-social-dto/ICaminhoDTO";
import {ITamanhoDTO} from "../../dto/rede-social-dto/ITamanhoDTO";
import IRelacaoDTO from "../../dto/relacao-dto/IRelacaoDTO";
import {ITamanhoRedeSocialTotalDTO} from "../../dto/rede-social-dto/ITamanhoRedeSocialTotalDTO";
import IRedeSocialArrayArray from "../../dto/rede-social-dto/IRedeSocialArrayArray";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ICaminhoEForcaLigacaoDTO} from "../../dto/rede-social-dto/ICaminhoEForcaLigacaoDTO";
import {ICaminhoFLigacaoRelacaoDTO} from "../../dto/rede-social-dto/ICaminhoLigRelDTO";
import {GrupoDTO} from "../../dto/utilizador-dto/GrupoDTO";


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'my-auth-token'
  })
};

@Injectable(
  {providedIn: 'root'}
)

export class RedeSocialService {


  constructor(private http: HttpClient, private usrService: UtilizadorService,private notification:MatSnackBar) {
  }

  routeRedeSocialPerspetiva = urlBaseMDR + "RedeSocial/";
  routeIACaminhos = urlBaseIA + "Caminhos/";
  routeIARedeSocial = urlBaseIA + "RedeSocial/";
  routeIACaminhosAStar = urlBaseIA + "GetCaminhoAStar/";

  getRedeSocialPerspetiva(nivel: number): Observable<Array<Array<IRelacaoDTO>>> {
    return this.http.get<Array<Array<IRelacaoDTO>>>(this.routeRedeSocialPerspetiva + this.usrService.userInfo.email + "/" + 3)
      .pipe(catchError(err => {
        if (err.status == 404) {
          this.mostrarNotificacao('PEDIDO PARA CONSULTAR REDE NÃO ENCONTRADO!',true);
        }
        if (err.status == 500) {
          this.mostrarNotificacao('ERRO DO MDR ou PARÂMETROS DE BUSCA INVÁLIDOS!',true);
        }
        if (err.status == 200) {
          this.mostrarNotificacao('REDE TRANSFERIDA COM SUCESSO!',false);
        }
        return throwError(err);
      }));
  }

  getRedeSocialPerspetivaDTO(nivel: number): Promise<IRedeSocialArrayArray | undefined> {
    const URL: string = this.routeRedeSocialPerspetiva + this.usrService.userInfo.email + "/" + nivel + "/" + "dto";
    return this.http.get<IRedeSocialArrayArray>(URL).toPromise();
  }


  getCaminhoMaisCurto(utilizadorDestino: string): Observable<ICaminhoDTO> {
    return this.http.get<ICaminhoDTO>(this.routeIACaminhos + "caminhoMaisCurto/" + this.usrService.userInfo.email + "/" + utilizadorDestino)
      .pipe(catchError(err => {
        if (err.status == 404) {
          this.mostrarNotificacao('PEDIDO PARA VER CAMINHO MAIS CURTO NÃO ENCONTRADO!',true);
        }
        if (err.status == 500) {
          this.mostrarNotificacao('ERRO DA IA ou PARÂMETROS DE BUSCA INVÁLIDOS!',true);
        }
        if (err.status == 200) {
          this.mostrarNotificacao('CAMINHO MAIS CURTO TRANSFERIDO COM SUCESSO!',false);
        }
        return throwError(err);
      }));

  }

  getCaminhoMaisForte(utilizadorDestino: string): Observable<ICaminhoDTO> {
    return this.http.get<ICaminhoDTO>(this.routeIACaminhos + "caminhoMaisForte/" + this.usrService.userInfo.email + "/" + utilizadorDestino)
      .pipe(catchError(err => {
        if (err.status == 404) {
          this.mostrarNotificacao('PEDIDO PARA VER CAMINHO MAIS FORTE NÃO ENCONTRADO!',true);
        }
        if (err.status == 500) {
          this.mostrarNotificacao('ERRO DA IA ou PARÂMETROS DE BUSCA INVÁLIDOS!',true);
        }
        if (err.status == 200) {
          this.mostrarNotificacao('CAMINHO MAIS FORTE TRANSFERIDO COM SUCESSO!',false);
        }
        return throwError(err);
      }));
  }

  getCaminhoMaisSeguro(utilizadorDestino: string, valorMinimo: number): Observable<ICaminhoDTO> {
    return this.http.get<ICaminhoDTO>(this.routeIACaminhos + "caminhoMaisSeguro/" + this.usrService.userInfo.email + "/" + utilizadorDestino + "/" + valorMinimo)
      .pipe(catchError(err => {
        if (err.status == 404) {
          this.mostrarNotificacao('PEDIDO PARA VER CAMINHO MAIS SEGURO NÃO ENCONTRADO!',true);
        }
        if (err.status == 500) {
          this.mostrarNotificacao('ERRO DA IA ou PARÂMETROS DE BUSCA INVÁLIDOS (CAMINHO IMPOSSÍVEL)!',true);
        }
        if (err.status == 200) {
          this.mostrarNotificacao('CAMINHO MAIS SEGURO TRANSFERIDO COM SUCESSO!',false);
        }
        return throwError(err);
      }));
  }

  getCaminhoByForca(utilizadorDestino: string, valorMaximo: number): Observable<ICaminhoEForcaLigacaoDTO> {
    return this.http.get<ICaminhoEForcaLigacaoDTO>(this.routeIACaminhos + "GetCaminhoAStar/" + this.usrService.userInfo.email + "/" + utilizadorDestino + "/"
      + valorMaximo + "/forcaLigacao").pipe(catchError(err => {
        if (err.status == 404) {
          this.mostrarNotificacao('PEDIDO PARA VER CAMINHO COM FORCA DE LIGACAO NÃO ENCONTRADO!',true);
        }
        if (err.status == 500) {
          this.mostrarNotificacao('ERRO DA IA ou PARÂMETROS DE BUSCA INVÁLIDOS (CAMINHO IMPOSSÍVEL)!',true);
        }
        if (err.status == 200) {
          this.mostrarNotificacao('CAMINHO COM FORCA DE LIGACAO TRANSFERIDO COM SUCESSO!',false);
        }
        return throwError(err);
      }));

  }

  getCaminhoByForcaRel(utilizadorDestino: string, valorMaximo: number): Observable<ICaminhoFLigacaoRelacaoDTO> {
    return this.http.get<ICaminhoFLigacaoRelacaoDTO>(this.routeIACaminhos + "GetCaminhoAStarMultiCriterio/" + this.usrService.userInfo.email + "/" + utilizadorDestino + "/"
      + valorMaximo+"/multiCriterio").pipe(catchError(err => {
      if (err.status == 404) {
        this.mostrarNotificacao('PEDIDO PARA VER CAMINHO COM FORCA DE LIGACAO E RELACAO NÃO ENCONTRADO!',true);
      }
      if (err.status == 500) {
        this.mostrarNotificacao('ERRO DA IA ou PARÂMETROS DE BUSCA INVÁLIDOS (CAMINHO IMPOSSÍVEL)!',true);
      }
      if (err.status == 200) {
        this.mostrarNotificacao('CAMINHO COM FORCA DE LIGACAO E RELACAO TRANSFERIDO COM SUCESSO!',false);
      }
      return throwError(err);
    }));

  }

  getFortalezaRede(): Observable<number> {
    return this.http.get<number>(this.routeIARedeSocial + "FortalezaRede/" + this.usrService.userInfo.email)
      .pipe(catchError(err => {
        if (err.status == 404) {
          this.mostrarNotificacao('PEDIDO PARA VER FORTALEZA DE REDE NÃO ENCONTRADO!',true);
        }
        if (err.status == 500) {
          this.mostrarNotificacao('ERRO DA IA ou PARÂMETROS DE BUSCA INVÁLIDOS!',true);
        }
        if (err.status == 200) {
          this.mostrarNotificacao('FORTALEZA DE REDE CALCULADA E DEVOLVIDA COM SUCESSO!',false);
        }
        return throwError(err);
      }));

  }

  getAmigosComuns(email: string): Observable<Array<string>> {
    return this.http.get<Array<string>>(this.routeIARedeSocial + "AmigosEmComum/" +
      this.usrService.userInfo.email + "/" + email)
      .pipe(catchError(err => {
        if (err.status == 404) {
          this.mostrarNotificacao('PEDIDO PARA VER AMIGOS COMUNS NÃO ENCONTRADO!',true);
        }
        if (err.status == 500) {
          this.mostrarNotificacao('ERRO DA IA ou PARÂMETROS DE BUSCA INVÁLIDOS!',true);
        }
        if (err.status == 200) {
          this.mostrarNotificacao('AMIGOS COMUNS ENCONTRADOS COM SUCESSO!',false);
        }
        return throwError(err);
      }));
  }

  getTamanhoRede(nivelColocado: number): Observable<ITamanhoDTO> {
    return this.http.get<ITamanhoDTO>(this.routeIARedeSocial + "TamanhoRedeUtilizador/" +
      this.usrService.userInfo.email + "/" + (nivelColocado-1))
      .pipe(catchError(err => {
        if (err.status == 404) {
          this.mostrarNotificacao('PEDIDO PARA VER TAMANHO DE REDE NÃO ENCONTRADO!',true);
        }
        if (err.status == 500) {
          this.mostrarNotificacao('ERRO DA IA ou PARÂMETROS DE BUSCA INVÁLIDOS!',true);
        }
        if (err.status == 200) {
          this.mostrarNotificacao('TAMANHO DE REDE CALCULADO E DEVOLVIDO COM SUCESSO!',false);
        }
        return throwError(err);
      }));
  }

  //RedeSocial/{email}/tamanhoRede
  getTotalUtilizadores(): Observable<ITamanhoRedeSocialTotalDTO> {
    return this.http.get<ITamanhoRedeSocialTotalDTO>(this.routeRedeSocialPerspetiva +
      this.usrService.userInfo.email + "/tamanhoRede")
      .pipe(catchError(err => {
        if (err.status == 404) {
          this.mostrarNotificacao('PEDIDO PARA VER NÚMERO TOTAL DE UTILIZADORES NÃO ENCONTRADO!',true);
        }
        if (err.status == 500) {
          this.mostrarNotificacao('ERRO DO MDR ou PARÂMETROS DE BUSCA INVÁLIDOS!',true);
        }
        if (err.status == 200) {
          this.mostrarNotificacao('NÚMERO TOTAL DE UTILIZADORES CALCULADO E DEVOLVIDO COM SUCESSO!',false);
        }
        return throwError(err);
      }));
  }

  sugerirGrupos(listaTags:Array<string>,n:number,t:number):Observable<GrupoDTO>{
    return this.http.post<GrupoDTO>(this.routeIARedeSocial+"SugestaoGrupos/"+this.usrService.userInfo.email+"/"
    +n+"/"+t,listaTags)
      .pipe(catchError(err => {
      if (err.status == 404) {
        this.mostrarNotificacao('SUGESTÃO DE GRUPOS NÃO ENCONTRADA!',true);
      }
      if (err.status == 500) {
        this.mostrarNotificacao('ERRO DO MDR ou PARÂMETROS DE BUSCA INVÁLIDOS!',true);
      }
      if (err.status == 200) {
        this.mostrarNotificacao('SUGESTÃO DE GRUPOS CARREGADA COM SUCESSO!',false);
      }
      return throwError(err);
    }));
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }


  //TODO:MUDAR URLS QUANDO ESTIVER FEITO ESTES 3 CAMINHOS NA IA
  getCaminhoMaisCurtoEmocional(utilizadorDestino: string,n:number): Observable<ICaminhoDTO> {
    return this.http.get<ICaminhoDTO>(this.routeIACaminhos + "GetCaminhoEstadosEmocionais/" + this.usrService.userInfo.email
      + "/" + utilizadorDestino+"/"+n+"/a-star-sentimentos")
      .pipe(catchError(err => {
        if (err.status == 404) {
          this.mostrarNotificacao('PEDIDO PARA VER CAMINHO MAIS CURTO NÃO ENCONTRADO!',true);
        }
        if (err.status == 500) {
          this.mostrarNotificacao('ERRO DA IA ou PARÂMETROS DE BUSCA INVÁLIDOS!',true);
        }
        if (err.status == 200) {
          this.mostrarNotificacao('CAMINHO MAIS CURTO TRANSFERIDO COM SUCESSO!',false);
        }
        return throwError(err);
      }));

  }

  ///api/Caminhos/GetCaminhoEstadosEmocionais/{origem}/{destino}/{N}/a-star-sentimentos
  getCaminhoMaisForteEmocional(utilizadorDestino: string,n:number): Observable<ICaminhoDTO> {
    return this.http.get<ICaminhoDTO>(this.routeIACaminhos + "GetCaminhoEstadosEmocionais/" + this.usrService.userInfo.email
      + "/" + utilizadorDestino+"/"+n+"/a-star-sentimentos")
      .pipe(catchError(err => {
        if (err.status == 404) {
          this.mostrarNotificacao('PEDIDO PARA VER CAMINHO MAIS FORTE NÃO ENCONTRADO!',true);
        }
        if (err.status == 500) {
          this.mostrarNotificacao('ERRO DA IA ou PARÂMETROS DE BUSCA INVÁLIDOS!',true);
        }
        if (err.status == 200) {
          this.mostrarNotificacao('CAMINHO MAIS FORTE TRANSFERIDO COM SUCESSO!',false);
        }
        return throwError(err);
      }));
  }

  getCaminhoMaisSeguroEmocional(utilizadorDestino: string, valorMinimo: number,n:number): Observable<ICaminhoDTO> {
    return this.http.get<ICaminhoDTO>(this.routeIACaminhos + "GetCaminhoAStarMulticriterio/" + this.usrService.userInfo.email
      + "/" + utilizadorDestino+"/"+n+"/a-star-sentimentos")
      .pipe(catchError(err => {
        if (err.status == 404) {
          this.mostrarNotificacao('PEDIDO PARA VER CAMINHO MAIS SEGURO NÃO ENCONTRADO!',true);
        }
        if (err.status == 500) {
          this.mostrarNotificacao('ERRO DA IA ou PARÂMETROS DE BUSCA INVÁLIDOS (CAMINHO IMPOSSÍVEL)!',true);
        }
        if (err.status == 200) {
          this.mostrarNotificacao('CAMINHO MAIS SEGURO TRANSFERIDO COM SUCESSO!',false);
        }
        return throwError(err);
      }));
  }
}
