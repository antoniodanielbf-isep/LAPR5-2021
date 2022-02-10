import {Component, NgZone, OnInit} from '@angular/core';
import {PedirIntroducaoService} from "../../services/pedido-intro-service/pedir-introducao.service";
import {Router} from '@angular/router';
import {FormBuilder, FormGroup, Validators, FormControl} from '@angular/forms'
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-create-pedido-introducao',
  templateUrl: './create-pedido-introducao.component.html',
  styleUrls: ['./create-pedido-introducao.component.css']
})
export class CreatePedidoIntroducaoComponent implements OnInit {

  constructor(public fb: FormBuilder,
              private ngZone: NgZone,
              private router: Router,
              public pedIntroService: PedirIntroducaoService,
              private notification: MatSnackBar
  ) { }

  pedIntroArray: any = [];
  pedidoIntroForm = new FormGroup({
    id: new FormControl('', Validators.required),
    descricao: new FormControl('', Validators.required),
    origem: new FormControl('', Validators.required),
    intermedio: new FormControl('', Validators.required),
    destino: new FormControl('', Validators.required),
    forca: new FormControl('', Validators.required),
    listTags: new FormControl('', Validators.required),
  })

  ngOnInit(): void {
  }

  submitForm() {
    this.mostrarNotificacao("A criar pedido de introdução:\n"+this.pedidoIntroForm.value,true);
    this.pedIntroService.createPedidoIntroducao(this.pedidoIntroForm.value).subscribe(res => {
      this.mostrarNotificacao("Sucesso:\n"+res,true);

      // this.ngZone.run(() => this.router.navigateByUrl(this.urlBase + '/login'));
    });
  }
  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
