import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import {urlBaseMDP} from "../../config";
import {catchError, Observable, throwError} from "rxjs";
import {UtilizadorService} from "../utilizador-service/utilizador.service";
import {IPostDTO} from "../../dto/post-dto/IPostDTO";
import {IComentarioDTO} from "../../dto/post-dto/IComentarioDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable(
  {providedIn: 'root'}
)

export class PostService {
  //@ts-ignore
  userInfo: IAuthDTO;

  constructor(private http: HttpClient, private usrService: UtilizadorService,private notification:MatSnackBar) {
    // @ts-ignore
    this.userInfo = localStorage.getItem("userInfo");
  }

  routeUrl = urlBaseMDP + "posts";
  routeComentariosUrl = urlBaseMDP + "comentarios";
  createPost(post: IPostDTO): Observable<IPostDTO> {
    return this.http.post<IPostDTO>(this.routeUrl, post).pipe(catchError(err => {
      if (err.status == 200) {
        this.mostrarNotificacao('POST EFETUADO COM SUCESSO!',false);
      }
      if (err.status == 400) {
        this.mostrarNotificacao('POST EFETUADO SEM SUCESSO!',true);
      }
      if (err.status == 500) {
        this.mostrarNotificacao('POST INVÁLIDO:\n DEVE TER PELO MENOS 1 TAG EM COMUM COM ESSE UTILIZADOR!\n OU JÁ FEZ UM PEDIDO DE LIGAÇÃO!',true);
      }
      return throwError(err);
    }));
  }


  enviarComentario(comentario :IComentarioDTO): Observable<IComentarioDTO> {
    return this.http.post<IComentarioDTO>(this.routeComentariosUrl, comentario).pipe(catchError(err => {
      if (err.status == 200) {
        this.mostrarNotificacao('COMENTARIO EFETUADO COM SUCESSO!',false);
      }
      if (err.status == 400) {
       this.mostrarNotificacao('COMENTARIO EFETUADO SEM SUCESSO!',true);
      }
      if (err.status == 500) {
        this.mostrarNotificacao('COMENTARIO INVÁLIDO!',true);
      }
      return throwError(err);
    }));
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }

}
