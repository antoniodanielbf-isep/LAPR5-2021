import {Component, OnInit} from '@angular/core';
import {UtilizadorService} from "../services/utilizador-service/utilizador.service";
import {Router} from "@angular/router";
import {LeaderboardService} from "../services/leaderboard-service/leaderboard.service";
import {ILeaderBoardDTO} from "../dto/leaderboard-dto/ILeaderBoardDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-leader-boards',
  templateUrl: './leader-boards.component.html',
  styleUrls: ['./leader-boards.component.css']
})
export class LeaderBoardsComponent implements OnInit {
  leaderBoardDimensaoCarregada: boolean = false;
  leaderBoardFortalezaCarregada: boolean = false;
  leaderBoardPontuacaoCarregada: boolean = false;

  leaderBoardDimensaoRede: ILeaderBoardDTO[] = [];
  leaderBoardFortalezaRede: ILeaderBoardDTO[] = [];
  leaderBoardPontuacaoRede: ILeaderBoardDTO[] = [];

  constructor(private utilizadorService: UtilizadorService,
              private leadService: LeaderboardService,
              private router: Router,
              private notification: MatSnackBar) {
  }

  ngOnInit(): void {
  }

  verLeaderboardDimensao() {
    this.leadService.getLeaderBoardDimensoesRede().subscribe(res => {
      if (res != null) {
        this.mostrarNotificacao('Leaderboard de dimensões de rede carregada com sucesso!', false);
        this.leaderBoardDimensaoRede = res;
        this.leaderBoardDimensaoCarregada = true;
      } else {
        this.mostrarNotificacao('Leaderboard de dimensões de rede não carregada!', true);

      }
    });
  }

  verLeaderboardFortaleza() {
    this.leadService.getLeaderBoardFortalezasRede().subscribe(res => {
      if (res != null) {
        this.mostrarNotificacao('Leaderboard de fortalezas de rede carregada com sucesso!', false);
        this.leaderBoardFortalezaRede = res;
        this.leaderBoardFortalezaCarregada = true;
      } else {
        this.mostrarNotificacao('Leaderboard de fortalezas de rede não carregada!', true);
      }
    });
  }

  verLeaderboardPontuacao() {
    this.leadService.getLeaderBoardPontuacoesRede().subscribe(res => {
      if (res != null) {
        this.mostrarNotificacao('Leaderboard de pontuacao carregada com sucesso!', false);
        this.leaderBoardPontuacaoRede = res;
        this.leaderBoardPontuacaoCarregada = true;
      } else {
        this.mostrarNotificacao('Leaderboard de pontuacao não carregada!', true);
      }
    });
  }


  voltarAoInicio() {
    this.router.navigate(['/inicio']);
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
