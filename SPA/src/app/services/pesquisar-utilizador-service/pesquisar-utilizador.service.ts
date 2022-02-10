import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import IPedidoLigacaoDTO from '../../dto/utilizador-dto/IPedidoLigacaoDTO';
import IPesquisaUtilizadorDTO from "../../dto/utilizador-dto/IPesquisaUtilizadorDTO";
import IUtilizadorDTO from "../../dto/utilizador-dto/IUtilizadorDTO";
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import {LoginService} from "../login-service/login.service";
import {urlBaseMDR} from "../../config";
import {catchError, Observable, throwError} from "rxjs";
import {UtilizadorService} from "../utilizador-service/utilizador.service";
import IIntroDTO from "../../dto/intro-dto/IIntroDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable(
  {providedIn: 'root'}
)

export class pesquisarUtilizadorService {
  //@ts-ignore
  userInfo: IAuthDTO;

  constructor(private http: HttpClient,private usrService:UtilizadorService,private notification:MatSnackBar) {
    // @ts-ignore
    this.userInfo = localStorage.getItem("userInfo");
  }

  routeUrl = urlBaseMDR + "Utilizador/";

  routeUrlPedido = urlBaseMDR + "PedidoLigacao/";


  createPedidoLigacao(pedidoLigacao: IPedidoLigacaoDTO):Observable<IPedidoLigacaoDTO> {
    return this.http.post<IPedidoLigacaoDTO>(this.routeUrlPedido + this.usrService.userInfo.email + "/createConnectionRequest"
      , pedidoLigacao) .pipe(catchError(err => {
      if(err.status==200){
        this.mostrarNotificacao('PEDIDO DE LIGAÇÃO FEITO COM SUCESSO!',false);
      }
      if(err.status==400){
        this.mostrarNotificacao('PEDIDO DE LIGAÇÃO FEITO SEM SUCESSO!',true);
      }
      if(err.status==500){
        this.mostrarNotificacao('PEDIDO DE LIGAÇÃO INVÁLIDO:\n DEVE TER PELO MENOS 1 TAG EM COMUM COM ESSE UTILIZADOR!\n OU JÁ FEZ UM PEDIDO DE LIGAÇÃO!',true);
      }
      return throwError(err);
    }));
  }

  searchUtilizador(utilizadorPesquisa: IPesquisaUtilizadorDTO): Observable<IUtilizadorDTO[]> {
    return this.http.post<IUtilizadorDTO[]>(this.routeUrl + this.userInfo + "/search", utilizadorPesquisa);
  }

  //tu tens o dto q mandas pós servidor e vai levar os outros daddos, se tu n metes o email tem q ir a string vaza

  createPedidoLigacaoComSugestaoAmizade(pedidoLigacao: IPedidoLigacaoDTO) {
    return this.http.post<IPedidoLigacaoDTO>(this.routeUrlPedido + this.usrService.userInfo.email + "/createConnectionRequest"
      , pedidoLigacao);
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
