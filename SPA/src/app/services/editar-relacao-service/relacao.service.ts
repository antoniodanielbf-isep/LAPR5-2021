import {HttpClient, HttpHeaders} from "@angular/common/http";
import {urlBaseMDR} from "../../config";
import {catchError, Observable} from "rxjs";
import {Injectable} from "@angular/core";
import IRelacaoDTO from "../../dto/relacao-dto/IRelacaoDTO";
import {UtilizadorService} from "../utilizador-service/utilizador.service";
import IRelacaoCriacaoDTO from "../../dto/relacao-dto/IRelacaoCriacaoDTO";
import {IRelacaoTagsDTO} from "../../dto/relacao-dto/IRelacaoTagsDTO";



const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'my-auth-token'
  })
};

@Injectable(
  {providedIn: 'root'}
)

export class RelacaoService{

  constructor(private http: HttpClient,public usrService: UtilizadorService) {
  }

  routeUrl = urlBaseMDR + "Relacao/";

  getRelacaoById(relacaoId:string): Observable<IRelacaoDTO> {
    return this.http.get<IRelacaoDTO>(this.routeUrl +relacaoId+"/getById");
  }

  getRelacoesUser(): Observable<IRelacaoDTO[]> {
    return this.http.get<IRelacaoDTO[]>(this.routeUrl + this.usrService.userInfo.email);
  }

  updateRelacao(relacao: IRelacaoCriacaoDTO, id: string){
    return this.http.put<IRelacaoDTO>(this.routeUrl + this.usrService.userInfo.email + "/" + id + "/changeAllInfo", relacao, httpOptions)
  .pipe(catchError(u => (u)));
  }

  // GET: api/Relacao/{email}/getTagsAllUsers
  getTagsAllUsers():Observable<IRelacaoTagsDTO> {
    return this.http.get<IRelacaoTagsDTO>(this.routeUrl + this.usrService.userInfo.email+"/getTagsAllUsers");
  }

  // GET: api/Relacao/{email}/getTagsUser
  getTagsUser():Observable<IRelacaoTagsDTO>{
    return this.http.get<IRelacaoTagsDTO>(this.routeUrl + this.usrService.userInfo.email+"/getTagsUser");
  }

}
