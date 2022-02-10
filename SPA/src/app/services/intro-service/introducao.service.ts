import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import IIntroDTO from "../../dto/intro-dto/IIntroDTO";
import {catchError, Observable, throwError} from "rxjs";
import IPedidoIntroDTO from "../../dto/pedido-intro-dto/IPedidoIntroDTO";
import IRelacaoDTO from "../../dto/relacao-dto/IRelacaoDTO";
import IRelacaoCriacaoDTO from "../../dto/relacao-dto/IRelacaoCriacaoDTO";
import {urlBaseMDR} from "../../config";
import {UtilizadorService} from "../utilizador-service/utilizador.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable(
  {providedIn: 'root'}
)

export class IntroducaoService {
  constructor(private http: HttpClient,private usrService:UtilizadorService,private notification:MatSnackBar) {
  }

  routeUrlIntroducao = urlBaseMDR+"Introducao/";
  routeURLRelacao = urlBaseMDR+"Relacao/";

  //api/Introducao/{email}
  getIntroducoesPendentesUtilizador():Observable<IIntroDTO[]>{
      return this.http.get<IIntroDTO[]>(this.routeUrlIntroducao + this.usrService.userInfo.email)
        .pipe(catchError(err => {
        if(err.status==404){
          this.mostrarNotificacao("NÃO TEM INTRODUÇÕES PENDENTE",true);
        }
        return throwError(err);
      }));
  }

  //ao aceitar uma introducao criamos uma relacao
  //api/Introducao/{emailDestino}/{introducaoId}/Accept
  aceitarIntroducao(introducaoPendente: IIntroDTO,relacaoIntroduzidaForm:IRelacaoCriacaoDTO):Observable<IRelacaoDTO> {
    return this.http.post<IRelacaoDTO>(this.routeUrlIntroducao+introducaoPendente.utilizadorDestino+"/" +
      introducaoPendente.introducaoId+"/Accept",relacaoIntroduzidaForm);
  }

  //api/Introducao/{email}/{introducaoId}/Decline
  rejeitarIntroducao(introducaoPendente: IIntroDTO):Observable<IIntroDTO> {
    return this.http.put<IIntroDTO>(this.routeUrlIntroducao + this.usrService.userInfo.email + "/"
      + introducaoPendente.introducaoId + "/Decline", introducaoPendente);
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
