import {Component, NgZone, OnInit} from '@angular/core';
import IUtilizadorFeedDTO from "../../dto/post-dto/IUtilizadorFeedDTO";
import {UtilizadorService} from "../../services/utilizador-service/utilizador.service";
import {Router} from "@angular/router";
import {IComentarioDTO} from "../../dto/post-dto/IComentarioDTO";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {PostService} from "../../services/post-service/post.service";
import {IUtilizadorIdDTO} from "../../dto/post-dto/IUtilizadorIdDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-feed-e-comentarios',
  templateUrl: './feed-e-comentarios.component.html',
  styleUrls: ['./feed-e-comentarios.component.css']
})
export class FeedEComentariosComponent implements OnInit {

  constructor(public userService: UtilizadorService, public postService: PostService,
              private ngZone: NgZone, private router: Router,
              private notification: MatSnackBar
  ) {
  }

  user: IUtilizadorIdDTO = {email: ''};

  ngOnInit(): void {

  }

  getFeedForm = new FormGroup({
    email: new FormControl('', Validators.required)
  });

  getFeedUtilizador() {
    this.user.email = this.getFeedForm.value.email;
    this.userService.getFeedUtilizador(this.user).subscribe(res => {
      if (res != null) {
        this.mostrarNotificacao("FEED DE POSTS OBTIDO sucesso!", false);
        this.feedUtilizador = res;
      } else {
        this.mostrarNotificacao("Erro ao obter Feed!", true);
      }
    });
  }

  feedUtilizador: IUtilizadorFeedDTO[] = [];
  comentarioUtilizador: IComentarioDTO = {reacao: '', texto: '', tags: '', utilizador: '', post: ''};
  comentarPostForm = new FormGroup({
    texto: new FormControl('', Validators.required),
    tags: new FormControl('', Validators.required)
  });

  verLikeEDislike: boolean = false;


  voltarInicio() {
    this.router.navigate(['/inicio']);
  }

  postarComentario(postId: string) {
    this.comentarioUtilizador.post = postId;
    this.comentarioUtilizador.texto = this.comentarPostForm.value.texto;
    this.comentarioUtilizador.tags = this.comentarPostForm.value.tags;
    this.comentarioUtilizador.utilizador = this.userService.userInfo.email;
    this.comentarioUtilizador.reacao = 'comentario';
    this.postService.enviarComentario(this.comentarioUtilizador).subscribe(res => {
      if (res != null) {
        this.mostrarNotificacao("Publicação comentada com sucesso!", false);

      } else {
        this.mostrarNotificacao("Erro ao comentar publicação!", true);

      }
    });
  }

  like(postId: string) {
    this.comentarioUtilizador.post = postId;
    this.comentarioUtilizador.texto = 'like';
    this.comentarioUtilizador.tags = 'reacao';
    this.comentarioUtilizador.utilizador = this.userService.userInfo.email;
    this.comentarioUtilizador.reacao = 'like';
    this.postService.enviarComentario(this.comentarioUtilizador).subscribe(res => {
      if (res != null) {
        this.mostrarNotificacao("Publicação comentada com sucesso!", false);
      }else {
        this.mostrarNotificacao("Erro ao comentar publicação!", true);

      }
    });
  }

  dislike(postId: string) {
    this.comentarioUtilizador.post = postId;
    this.comentarioUtilizador.texto = 'dislike';
    this.comentarioUtilizador.tags = 'reacao';
    this.comentarioUtilizador.utilizador = this.userService.userInfo.email;
    this.comentarioUtilizador.reacao = 'dislike';
    this.postService.enviarComentario(this.comentarioUtilizador).subscribe(res => {
      if (res != null) {
        this.mostrarNotificacao("Publicação comentada com sucesso!", false);
      }else {
        this.mostrarNotificacao("Erro ao comentar publicação!", true);

      }
    });
  }

  postarReacao() {
    this.verLikeEDislike = true;
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }
}
