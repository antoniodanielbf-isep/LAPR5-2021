import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {CreateUtilizadorComponent} from "./utilizador/create-utilizador/create-utilizador.component";
import {LoginComponent} from "./login/login.component";
import {InicioComponent} from "./inicio/inicio.component";
import {ChangeUtilizadorInfoComponent} from "./utilizador/change-utilizador-info/change-utilizador-info.component";
import {PesquisarUtilizadorComponent} from "./utilizador/pesquisar-utilizador/pesquisar-utilizador.component";
import {AprovarDesaprovarPedidoIntroducaoComponent} from "./pedido-introducao/aprovar-desaprovar-pedido-introducao/aprovar-desaprovar-pedido-introducao.component";
import {EditarRelacaoComponent} from "./relacao/editar-relacao/editar-relacao.component";
import {AceitarRejeitarIntroducaoComponent} from "./introducao/aceitar-rejeitar-introducao.component";
import {PedirIntroducaoUtilizadorObjetivoComponent} from "./pedido-introducao/pedir-introducao-utilizador-objetivo/pedir-introducao-utilizador-objetivo.component";
import {CreatePedidoIntroducaoComponent} from "./pedido-introducao/create-pedido-introducao/create-pedido-introducao.component";
import {AlterarHumorUtilizadorComponent} from "./utilizador/alterar-humor-utilizador/alterar-humor-utilizador.component";
import {EscolherUtilizadoresObjetivoComponent} from "./pedido-introducao/escolher-utilizadores-objetivo/escolher-utilizadores-objetivo.component";
import {ObterPedidosLigacaoPendentesComponent} from "./pedido-ligacao/obter-pedidos-ligacao-pendentes/obter-pedidos-ligacao-pendentes.component";
import {RedeSocialPerspetivaComponent} from "./rede-social-perspetiva/rede-social-perspetiva.component";
import {TermosComponent} from "./utilizador/create-utilizador/termos/termos.component";
import {TagsCloudComponent} from "./tags-cloud/tags-cloud.component";
import {CriarPostComponent} from "./post/criar-post/criar-post.component";
import {AmigosComunsEntreDoisUsersComponent} from "./amigos/amigos-comuns-entre-dois-users/amigos-comuns-entre-dois-users.component";
import {FeedEComentariosComponent} from "./post/feed-e-comentarios/feed-e-comentarios.component";
import {LeaderBoardsComponent} from "./leader-boards/leader-boards.component";
import {RedeSocialComponent} from "./rede-social/rede-social.component";

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'registo', component: CreateUtilizadorComponent},
  {path: 'inicio',component:InicioComponent},
  {path: 'editarInfo',component:ChangeUtilizadorInfoComponent},
  {path: 'pesquisarUser',component:PesquisarUtilizadorComponent},
  {path: 'aprovarDesaprovarPedIntro',component:AprovarDesaprovarPedidoIntroducaoComponent},
  {path: 'editarRelacao',component:EditarRelacaoComponent},
  {path: 'aceitarRejeitarIntro',component:AceitarRejeitarIntroducaoComponent},
  {path: 'pedirIntroducao',component:PedirIntroducaoUtilizadorObjetivoComponent},
  {path: 'criarIntro',component:CreatePedidoIntroducaoComponent},
  {path: 'escolherUtilizadoresObjetivo',component:EscolherUtilizadoresObjetivoComponent},
  {path: 'obterPedidosLigacaoPendentes',component:ObterPedidosLigacaoPendentesComponent},
  {path: 'redeSocialPerspetiva', component:RedeSocialPerspetivaComponent},
  {path: 'termos',component:TermosComponent},
  {path: 'tagsCloud',component:TagsCloudComponent},
  {path: 'criar-post',component:CriarPostComponent},
  {path: 'amigosComuns',component:AmigosComunsEntreDoisUsersComponent},
  {path: 'verFeed',component:FeedEComentariosComponent},
  {path: 'leaderboards',component:LeaderBoardsComponent},
  {path: 'rede-social', component:RedeSocialComponent},

];

@NgModule({
  declarations: [],
  imports: [RouterModule,
    RouterModule.forRoot(routes),
    CommonModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
