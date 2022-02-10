import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import IUtilizadorDTO from '../../dto/utilizador-dto/IUtilizadorDTO';
import {urlBaseIA, urlBaseMDP, urlBaseMDR} from '../../config';
import {catchError, map, Observable, throwError} from "rxjs";
import {LoginService} from "../login-service/login.service";
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import IUtilizadorSugestoesDTO from "../../dto/utilizador-dto/IUtilizadorSugestoesDTO";
import IRelacaoCriacaoDTO from "../../dto/relacao-dto/IRelacaoCriacaoDTO";
import IRelacaoDTO from "../../dto/relacao-dto/IRelacaoDTO";
import IAlterarHumorDTO from "../../dto/utilizador-dto/IAlterarHumorDTO";
import {IUtilizadorMDPDTO} from "../../dto/utilizador-dto/IUtilizadorMDPDTO";
import {IUtilizadorTagsDTO} from "../../dto/utilizador-dto/IUtilizadorTagsDTO";
import IUtilizadorFeedDTO from "../../dto/post-dto/IUtilizadorFeedDTO";
import {IUtilizadorSugestoesSendDTO} from "../../dto/utilizador-dto/IUtilizadorSugestoesSendDTO";
import {IUtilizadorIdDTO} from "../../dto/post-dto/IUtilizadorIdDTO";
import IUtilizadorLikeDislikeDTO from "../../dto/utilizador-dto/IUtilizadorLikeDislikeDTO";
import IUtilizadorOrDestDTO from "../../dto/utilizador-dto/IUtilizadorOrDestDTO";
import {IUtilizadorEraseDTO} from "../../dto/utilizador-dto/IUtilizadorEraseDTO";
import {IUtilizadorEraseMDRDTO} from "../../dto/utilizador-dto/IUtilizadorEraseMDRDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'my-auth-token'
  })
};

@Injectable(
  {providedIn: 'root'}
)

export class UtilizadorService {

  userInfo: IAuthDTO={email:''};

  constructor(private http: HttpClient,
              private loginService: LoginService,
              private notification:MatSnackBar) {
    // @ts-ignore
    this.userInfo.email = localStorage.getItem("userInfo");
  }

  routeUrl = urlBaseMDR + "Utilizador";
  routeURLMDP = urlBaseMDP +"utilizadores";
  routeURLIA = urlBaseIA;

  createUtilizador(utilizador: IUtilizadorDTO) {
    return this.http.post<IUtilizadorDTO>(this.routeUrl, utilizador, httpOptions)
      .pipe(catchError(u => (u)));
  }

  createUtilizadorMDP(utilizador:IUtilizadorMDPDTO) {
    return this.http.post<IUtilizadorMDPDTO>(this.routeURLMDP, utilizador, httpOptions)
      .pipe(catchError(err => {
        if(err.status==404){
          this.mostrarNotificacao('POST UTILIZADOR NÃO ENCONTRADO NO MDP!',true);
        }
        if(err.status==500){
          this.mostrarNotificacao('POST UTILIZADOR COM PARÂMETROS ERRADOS NO MDP!',true);
        }
        if(err.status==200){
          this.mostrarNotificacao('POST UTILIZADOR BEM SUCEDIDO NO MDP!',false);
        }
        return throwError(err);
      }));
  }

  getUtilizadores(): Promise<IUtilizadorDTO[] | undefined> {
    return this.http.get<IUtilizadorDTO[]>(this.routeUrl).toPromise();
  }

  async getUtilizador(): Promise<IUtilizadorDTO> {
    // @ts-ignore
    return this.http.get<IUtilizadorDTO>(this.routeUrl + "/" + this.userInfo.email).toPromise();
  }

   getUtilizadorById(email:string): Observable<IUtilizadorDTO> {
    return this.http.get<IUtilizadorDTO>(this.routeUrl + "/" + email).
    pipe(map((res:IUtilizadorDTO)=>{
      return res;
    }));
  }
  async getUtilizadorBYId(email:string): Promise<IUtilizadorDTO | undefined> {
    return this.http.get<IUtilizadorDTO>(this.routeUrl + "/" + email).toPromise();
  }
  updateUtilizador(utilizador: IUtilizadorDTO) {
    return this.http.put<IUtilizadorDTO>(this.routeUrl + "/" + this.userInfo.email + "/changeAllInfo", utilizador, httpOptions)
      .pipe(catchError(u => (u)));
  }

  updateEstadoHumorUtilizador(humor: IAlterarHumorDTO) {
    return this.http.put<IUtilizadorDTO>(this.routeUrl + "/" + this.userInfo.email + "/changeHumor", humor, httpOptions)
      .pipe(catchError(u => (u)));

  }

  public getSugestoesUtilizadores():Observable<IUtilizadorSugestoesDTO[]> {
    return this.http.get<IUtilizadorSugestoesDTO[]>(this.routeUrl + "/" + this.userInfo.email + "/sugestoesAmizade")
      .pipe(map((res:IUtilizadorSugestoesDTO[])=>{
        return res;
      }))
  }

  // GET: api/Utilizador/{email}/allTagsAllUsers
  getTagsAllUsers():Observable<IUtilizadorTagsDTO> {
    return this.http.get<IUtilizadorTagsDTO>(this.routeUrl + "/" + this.userInfo.email + "/allTagsAllUsers");
  }

  // GET: api/Utilizador/{email}/allTagsUser
  getTagsUser():Observable<IUtilizadorTagsDTO> {
    return this.http.get<IUtilizadorTagsDTO>(this.routeUrl + "/" + this.userInfo.email + "/allTagsUser");
  }

  getFeedUtilizador(user:IUtilizadorIdDTO):Observable<IUtilizadorFeedDTO[]> {
    return this.http.post<IUtilizadorFeedDTO[]>(this.routeURLMDP+"/getFeed",user)
      .pipe(catchError(err => {
      if(err.status==404){
        this.mostrarNotificacao('FEED UTILIZADOR NÃO ENCONTRADO NO MDP!',true);
      }
      if(err.status==500){
        this.mostrarNotificacao('FEED UTILIZADOR COM PARÂMETROS ERRADOS NO MDP!',true);
      }
      if(err.status==200){
        this.mostrarNotificacao('FEED UTILIZADOR BEM SUCEDIDO NO MDP!',false);
      }
      return throwError(err);
    }));
  }


  getSugestoesAmizade() {
    return this.http.get<IUtilizadorSugestoesSendDTO[]>(this.routeURLIA + "/" + this.userInfo.email + "/sugestoesConexao")
      .pipe(map((res:IUtilizadorSugestoesSendDTO[])=>{
        return res;
      }))
  }

  calcularForcaLigacao(users:IUtilizadorOrDestDTO):Observable<IUtilizadorLikeDislikeDTO> {
    return this.http.post<IUtilizadorLikeDislikeDTO>(this.routeURLMDP+"/getStrength",users)
      .pipe(catchError(err => {
        if(err.status==404){
          this.mostrarNotificacao('CÁLCULO FORÇA LIGAÇÃO NÃO ENCONTRADO NO MDP!',true);
        }
        if(err.status==500){
          this.mostrarNotificacao('UTILIZADOR COM PARÂMETROS ERRADOS NO MDP!',true);
        }
        return throwError(err);
      }));
  }

  apagarUtilizadorMDR() {
    return this.http.put<IUtilizadorEraseMDRDTO>(this.routeUrl+"/"+this.userInfo.email+"/eraseUser",this.userInfo.email)
      .pipe(catchError(err => {
        if(err.status==404){
          this.mostrarNotificacao('ERASE DO UTILIZADOR NÃO ENCONTRADO NO MDR!',true);
        }
        if(err.status==500){
          this.mostrarNotificacao('UTILIZADOR COM PARÂMETROS ERRADOS NO MDR!',true);
        }
        return throwError(err);
      }));
  }

  apagarUtilizadorMDP(utilizador:IUtilizadorEraseDTO):Observable<IUtilizadorDTO> {
    return this.http.post<IUtilizadorDTO>(this.routeURLMDP+"/erase",utilizador)
      .pipe(catchError(err => {
        if(err.status==404){
          this.mostrarNotificacao('ERASE DO UTILIZADOR NÃO ENCONTRADO NO MDP!',true);
        }
        if(err.status==500){
          this.mostrarNotificacao('UTILIZADOR COM PARÂMETROS ERRADOS NO MDP!',true);
        }
        return throwError(err);
      }));
  }

  getSugestoesUtilizadoresParaPedirIntroducao():Observable<IUtilizadorSugestoesDTO[]> {
    return this.http.get<IUtilizadorSugestoesDTO[]>(this.routeUrl + "/" + this.userInfo.email + "/sugestoesUtilizadoresParaPedirIntroducao")
      .pipe(map((res:IUtilizadorSugestoesDTO[])=>{
        return res;
      }))
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
