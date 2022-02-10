import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-utilizador',
  templateUrl: './utilizador.component.html',
  styleUrls: ['./utilizador.component.css']
})
export class UtilizadorComponent implements OnInit {
  validUtilizadorLinks = [];

  constructor() {
  }

  ngOnInit(): void {
    this.getValidLinks();
  }

  private getValidLinks() {
    for (const link of this.navUtilizadorLinks) {
      // @ts-ignore
      this.validUtilizadorLinks.push(link);
    }
  }

  navUtilizadorLinks = [{
    path: 'create-user',
    label: 'Create',
    icon: 'add_circle_outline',
    roles: ['Client', 'Administrator', 'Manager']
  }, {
    path: 'list-users', label: 'List', icon: 'list', roles: ['Administrator']
  }]

  activeLink: any;
}
