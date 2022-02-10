import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import IPedidoIntroDTO from "../../dto/pedido-intro-dto/IPedidoIntroDTO";
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import {catchError, Observable, throwError} from "rxjs";
import IIntroDTO from "../../dto/intro-dto/IIntroDTO";
import IPedidoIntroCriadoDTO from "../../dto/pedido-intro-dto/IPedidoIntroCriadoDTO";
import {IIntermediarioDTO} from "../../dto/pedido-intro-dto/IIntermediarioDTO";
import IUtilizadorDTO from "../../dto/utilizador-dto/IUtilizadorDTO";
import {UtilizadorService} from "../utilizador-service/utilizador.service";
import {urlBaseMDR} from "../../config";
import {IPedidoIntroAceiteDTO} from "../../dto/pedido-intro-dto/IPedidoIntroAceiteDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable(
  {providedIn: 'root'}
)

export class PedirIntroducaoService {
  constructor(private http: HttpClient,private usrService:UtilizadorService,private notification:MatSnackBar) {
    //@ts-ignore
    this.userInfo = localStorage.getItem('userInfo');
  }

  public userInfo: IAuthDTO;

  routeUrlPedido = urlBaseMDR+"PedidoIntroducao/";
  routeUrlIntroducao = urlBaseMDR+"Introducao/";

  createPedidoIntroducao(pedidoIntro: IPedidoIntroCriadoDTO):Observable<any> {
    pedidoIntro.estadoPedidoIntroducao = 2;
    return this.http.post<IPedidoIntroDTO>(this.routeUrlPedido + this.usrService.userInfo.email
      + "/makeRequest", pedidoIntro).pipe(catchError(err => {
        if(err.status==500){
          this.mostrarNotificacao('PEDIDO JÁ FEITO OU RECUSADO!',true);
        }
      return throwError(err);
    }));
  }

  getPedidoIntroducao(): Promise<IPedidoIntroDTO[] | undefined> {
    return this.http.get<IPedidoIntroDTO[]>(this.routeUrlPedido).toPromise();
  }

  getPedidoIntroducaoUtilizador(emailUtilizador: string): Promise<IPedidoIntroDTO[] | undefined> {
    return this.http.get<IPedidoIntroDTO[]>(this.routeUrlPedido + emailUtilizador).toPromise();
  }

  getPedidosIntroducaoPendentesUtilizador(): Observable<IPedidoIntroDTO[]> {
    return this.http.get<IPedidoIntroDTO[]>(this.routeUrlPedido + this.usrService.userInfo.email + "/intermediate")
      .pipe(catchError(err => {
      if(err.status==404){
        this.mostrarNotificacao('NÃO TEM PEDIDOS DE INTRODUÇÃO PENDENTES!',true);
      }
      return throwError(err);
    }));
  }

  //api/PedidoIntroducao/{email}/acceptRequest
  aceitarPedidoIntroducao(pedidoIntroducaoAceite: IPedidoIntroAceiteDTO): Observable<IIntroDTO> {
    return this.http.post<IIntroDTO>(this.routeUrlPedido + this.usrService.userInfo.email + "/acceptRequest", pedidoIntroducaoAceite);
  }

  //api/PedidoIntroducao/{email}/{pedidoId}/Decline
  rejeitarPedidoIntroducao(pedidoIntroducaoPendente: IPedidoIntroDTO): Observable<IPedidoIntroDTO> {
    return this.http.put<IPedidoIntroDTO>(this.routeUrlPedido + this.usrService.userInfo.email + "/"
      + pedidoIntroducaoPendente.id + "/Decline", pedidoIntroducaoPendente);
  }

  async getUtilizadorIntermediario(intermediario:string):Promise<IIntermediarioDTO|undefined> {
    return this.http.get<IIntermediarioDTO>(this.routeUrlPedido +"obterIntermediario/"+ this.usrService.userInfo.email
    + "/"+intermediario).toPromise();
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
