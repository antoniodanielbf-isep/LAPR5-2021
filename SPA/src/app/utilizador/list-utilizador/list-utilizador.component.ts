import {Component, OnInit} from '@angular/core';
import {UtilizadorService} from "../../services/utilizador-service/utilizador.service";
import IUtilizadorDTO from "../../dto/utilizador-dto/IUtilizadorDTO";

@Component({
  selector: 'app-list-utilizador',
  templateUrl: './list-utilizador.component.html',
  styleUrls: ['./list-utilizador.component.css']
})

export class ListUtilizadorComponent implements OnInit {

  constructor(private utilizadorService: UtilizadorService) {
  }

  listUsers: Array<IUtilizadorDTO> = [];

  private async getUtilizadores() {
    var list = await this.utilizadorService.getUtilizadores();
    // @ts-ignore
    this.listUsers = list?.sort((user1, user2) => {
      var number1: number = +user1.estadoEmocionalUtilizador;
      var number2: number = +user2.estadoEmocionalUtilizador;
      if (number1 > number2) {
        return 1;
      }
      if (number1 < number2) {
        return -1;
      }
      return 0;
    });
  }

  ngOnInit(): void {
    this.getUtilizadores();
  }
}
