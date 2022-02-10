import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CriarPostComponent } from './criar-post/criar-post.component';
import {MatFormFieldModule} from "@angular/material/form-field";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import { FeedEComentariosComponent } from './feed-e-comentarios/feed-e-comentarios.component';



@NgModule({
  declarations: [
  ],
    imports: [
        CommonModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        MatInputModule,
        FormsModule
    ]
})
export class PostModule { }
