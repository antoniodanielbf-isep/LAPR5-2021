import {Component, NgZone, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {UtilizadorService} from "../../services/utilizador-service/utilizador.service";
import {PostService} from "../../services/post-service/post.service";
import {IPostDTO} from "../../dto/post-dto/IPostDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-criar-post',
  templateUrl: './criar-post.component.html',
  styleUrls: ['./criar-post.component.css']
})

export class CriarPostComponent implements OnInit {

  constructor(public pf: FormBuilder, private ngZone: NgZone,
              private router: Router,
              public usrService: UtilizadorService,
              public postService: PostService,
              private notification: MatSnackBar) {
  }

  post: IPostDTO = {texto: '', tags: [''], utilizador: ''};

  postForm = new FormGroup({
    texto: new FormControl('', Validators.required),
    tags: new FormControl('',)
  })


  ngOnInit(): void {
  }

  criarPost() {
    this.post.texto = this.postForm.value.texto;
    this.post.tags = this.postForm.value.tags;
    this.post.utilizador = this.usrService.userInfo.email;

    this.postService.createPost(this.post).subscribe(res => {
      if (res != null) {
        this.mostrarNotificacao('Post Efetuado com sucesso!',false)
      }else{
        this.mostrarNotificacao('Post não efetuado!',true)
      }
    });
  }

  voltarInicio() {
    this.router.navigate(['/inicio']);
  }
  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
