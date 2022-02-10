import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pedir-introducao',
  templateUrl: './pedir-introducao.component.html',
  styleUrls: ['./pedir-introducao.component.css']
})
export class PedirIntroducaoComponent implements OnInit {

  validPedidoIntroducaoLinks = [];

  constructor() { }

  ngOnInit(): void {
    this.getValidLinks();
  }

  private getValidLinks() {
    for (const link of this.navPedidoIntroducaoLinks) {
      // @ts-ignore
      this.validPedidoIntroducaoLinks.push(link);
    }
  }

  navPedidoIntroducaoLinks = [{
    path: 'pedir-introducao',
    label: 'Create',
    icon: 'add_circle_outline',
    roles: ['Client', 'Administrator', 'Manager']
  }]

  activeLink: any;
}
