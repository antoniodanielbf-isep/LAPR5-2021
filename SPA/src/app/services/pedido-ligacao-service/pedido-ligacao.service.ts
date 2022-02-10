import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import IPedidoIntroCriadoDTO from "../../dto/pedido-intro-dto/IPedidoIntroCriadoDTO";
import IPedidoIntroDTO from "../../dto/pedido-intro-dto/IPedidoIntroDTO";
import {Observable} from "rxjs";
import IIntroDTO from "../../dto/intro-dto/IIntroDTO";
import {urlBaseMDR} from "../../config";
import IPedidoLigacaoTodaDTO from "../../dto/pedido-ligacao-dto/IPedidoLigacaoTodaDTO";
import {UtilizadorService} from "../utilizador-service/utilizador.service";
import IRelacaoCriacaoDTO from "../../dto/relacao-dto/IRelacaoCriacaoDTO";
import IRelacaoDTO from "../../dto/relacao-dto/IRelacaoDTO";
import {IPedidoLigacaoAceiteDTO} from "../../dto/pedido-ligacao-dto/IPedidoLigacaoAceiteDTO";

@Injectable(
  {providedIn: 'root'}
)

export class PedidoLigacaoService {
  constructor(private http: HttpClient,private userService:UtilizadorService) {
    //@ts-ignore
    this.userInfo = localStorage.getItem("userInfo");
  }

  public userInfo: IAuthDTO;

  routeUrlPedidoLigacao = urlBaseMDR+"PedidoLigacao/";


  getPedidosLigacaoPendentes(): Promise<IPedidoLigacaoTodaDTO[] | undefined> {
    return this.http.get<IPedidoLigacaoTodaDTO[]>(this.routeUrlPedidoLigacao+this.userService.userInfo.email+"/destiny").toPromise();
  }

  rejeitarLigacao(pedidoLigacaoPendente: IPedidoLigacaoTodaDTO):Observable<IIntroDTO> {
    return this.http.put<IIntroDTO>(this.routeUrlPedidoLigacao + this.userService.userInfo.email + "/"
      + pedidoLigacaoPendente.id + "/Decline", pedidoLigacaoPendente);
  }

  //ao aceitar um pedido de Ligação criamos uma relacao
  //POST: api/PedidoLigacao/{emailDestinatario}/Aceitar
  aceitarLigacao(pedidoLigacaoPendenteId:string,pedidoLigacaoAceiteDTO:IPedidoLigacaoAceiteDTO):Observable<IRelacaoDTO> {
    return this.http.post<IRelacaoDTO>(this.routeUrlPedidoLigacao+this.userService.userInfo.email+"/"+
      pedidoLigacaoPendenteId+ "/Aceitar",pedidoLigacaoAceiteDTO);
  }
}
