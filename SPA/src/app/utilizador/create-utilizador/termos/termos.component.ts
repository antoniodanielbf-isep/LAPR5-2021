import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-termos',
  templateUrl: './termos.component.html',
  styleUrls: ['./termos.component.css']
})
export class TermosComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit(): void {
  }

  voltarAoRegisto(){
    this.router.navigate(['/registo']);
  }

}
