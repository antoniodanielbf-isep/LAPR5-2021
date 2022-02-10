import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import {MatListModule} from "@angular/material/list";
import { UtilizadorComponent } from './utilizador/utilizador.component';
import { CreateUtilizadorComponent } from './utilizador/create-utilizador/create-utilizador.component';
import { ListUtilizadorComponent } from './utilizador/list-utilizador/list-utilizador.component';
import {MatTabsModule} from "@angular/material/tabs";
import {MatIconModule} from "@angular/material/icon";
import {MatExpansionModule} from "@angular/material/expansion";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import { EditarRelacaoComponent } from './relacao/editar-relacao/editar-relacao.component';
import { PedirIntroducaoComponent } from './pedido-introducao/pedir-introducao.component';
import { PesquisarUtilizadorComponent } from './utilizador/pesquisar-utilizador/pesquisar-utilizador.component';
import { RelacaoComponent } from './relacao/relacao.component';
import { ChangeUtilizadorInfoComponent } from './utilizador/change-utilizador-info/change-utilizador-info.component';
import { CreatePedidoIntroducaoComponent } from './pedido-introducao/create-pedido-introducao/create-pedido-introducao.component';
import { LoginComponent } from './login/login.component';
import { InicioComponent } from './inicio/inicio.component'
import {MatButtonModule} from "@angular/material/button";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {AprovarDesaprovarPedidoIntroducaoComponent} from "./pedido-introducao/aprovar-desaprovar-pedido-introducao/aprovar-desaprovar-pedido-introducao.component";
import {AceitarRejeitarIntroducaoComponent} from "./introducao/aceitar-rejeitar-introducao.component";
import {PedirIntroducaoUtilizadorObjetivoComponent} from "./pedido-introducao/pedir-introducao-utilizador-objetivo/pedir-introducao-utilizador-objetivo.component";
import { AlterarHumorUtilizadorComponent } from './utilizador/alterar-humor-utilizador/alterar-humor-utilizador.component';
import {EscolherUtilizadoresObjetivoComponent} from "./pedido-introducao/escolher-utilizadores-objetivo/escolher-utilizadores-objetivo.component";
import {PedidoIntroducaoModule} from "./pedido-introducao/pedido-introducao.module";
import {ObterPedidosLigacaoPendentesComponent} from "./pedido-ligacao/obter-pedidos-ligacao-pendentes/obter-pedidos-ligacao-pendentes.component";
import {RedeSocialPerspetivaComponent} from "./rede-social-perspetiva/rede-social-perspetiva.component";
import { TermosComponent } from './utilizador/create-utilizador/termos/termos.component';
import {MatCheckboxModule} from "@angular/material/checkbox";
import { TagsCloudComponent } from './tags-cloud/tags-cloud.component';
import {CriarPostComponent} from "./post/criar-post/criar-post.component";
import {AmigosComunsEntreDoisUsersComponent} from "./amigos/amigos-comuns-entre-dois-users/amigos-comuns-entre-dois-users.component";
import { LeaderBoardsComponent } from './leader-boards/leader-boards.component';
import {FeedEComentariosComponent} from "./post/feed-e-comentarios/feed-e-comentarios.component";
import { RedeSocialComponent } from './rede-social/rede-social.component';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        BrowserAnimationsModule,
        ReactiveFormsModule,
        RouterModule,
        AppRoutingModule,
        MatListModule,
        MatTabsModule,
        MatIconModule,
        MatExpansionModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatButtonModule,
        MatSnackBarModule,
        PedidoIntroducaoModule,
        MatCheckboxModule,
    ],
  declarations:[
    AppComponent,
    UtilizadorComponent,
    CreateUtilizadorComponent,
    ListUtilizadorComponent,
    EditarRelacaoComponent,
    PedirIntroducaoComponent,
    PesquisarUtilizadorComponent,
    RelacaoComponent,
    ChangeUtilizadorInfoComponent,
    CreatePedidoIntroducaoComponent,
    LoginComponent,
    InicioComponent,
    AprovarDesaprovarPedidoIntroducaoComponent,
    AceitarRejeitarIntroducaoComponent,
    PedirIntroducaoUtilizadorObjetivoComponent,
    AlterarHumorUtilizadorComponent,
    EscolherUtilizadoresObjetivoComponent,
    ObterPedidosLigacaoPendentesComponent,
    RedeSocialPerspetivaComponent,
    TermosComponent,
    TagsCloudComponent,
    CriarPostComponent,
    AmigosComunsEntreDoisUsersComponent,
    FeedEComentariosComponent,
    LeaderBoardsComponent,
    RedeSocialComponent
  ],
  exports:[RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
