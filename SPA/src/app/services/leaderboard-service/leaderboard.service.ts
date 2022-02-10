import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {catchError, Observable, throwError} from "rxjs";
import {urlBaseMDR} from "../../config";
import {UtilizadorService} from "../utilizador-service/utilizador.service";
import {ILeaderBoardDTO} from "../../dto/leaderboard-dto/ILeaderBoardDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

@Injectable(
  {providedIn: 'root'}
)

export class LeaderboardService {
  constructor(private http: HttpClient,private usrService:UtilizadorService,private notification:MatSnackBar) {
  }

  routeUrlLeaderBoard = urlBaseMDR+"LeaderBoard/";

  //api/LeaderBoard/{email}/strength
  getLeaderBoardFortalezasRede():Observable<ILeaderBoardDTO[]>{
    return this.http.get<ILeaderBoardDTO[]>(this.routeUrlLeaderBoard + this.usrService.userInfo.email+"/strength")
      .pipe(catchError(err => {
        if(err.status==404){
         this.mostrarNotificacao('NÃO EXISTEM FORTALEZAS DE REDE!',true);
        }
        if(err.status==500){
          this.mostrarNotificacao('FORTALEZAS DE REDE INVÁLIDAS, AUMENTE A SUA REDE!',true);
        }
        return throwError(err);
      }));
  }

  getLeaderBoardDimensoesRede():Observable<ILeaderBoardDTO[]>{
    return this.http.get<ILeaderBoardDTO[]>(this.routeUrlLeaderBoard + this.usrService.userInfo.email+"/dimension")
      .pipe(catchError(err => {
        if(err.status==404){
          this.mostrarNotificacao('NÃO EXISTEM DIMENSÕES DE REDE!',true);
        }
        if(err.status==500){
          this.mostrarNotificacao('DIMENSÕES DE REDE INVÁLIDAS, AUMENTE A SUA REDE!',true);
        }
        return throwError(err);
      }));
  }

  getLeaderBoardPontuacoesRede():Observable<ILeaderBoardDTO[]>{
    return this.http.get<ILeaderBoardDTO[]>(this.routeUrlLeaderBoard + this.usrService.userInfo.email+"/points")
      .pipe(catchError(err => {
        if(err.status==404){
          this.mostrarNotificacao('NÃO EXISTEM PONTUAÇÕES!',true);
        }
        if(err.status==500){
          this.mostrarNotificacao('DIMENSÕES DE REDE INVÁLIDAS, AUMENTE A SUA REDE!',true);
        }
        return throwError(err);
      }));
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
