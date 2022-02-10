import {ChangeDetectorRef, Component} from '@angular/core';
import {Subscription} from 'rxjs';

//importar todos os componentes para a app principal

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Jogo Rede Social';
  isCollapsed = false;

  constructor(private cdr: ChangeDetectorRef) {
  }

  ngOnInit() {
    this.cdr.detectChanges();
  }

  ngOnDestroy() {
  }
}
