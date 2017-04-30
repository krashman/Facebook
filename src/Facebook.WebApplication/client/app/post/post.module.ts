import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostComponent } from './post.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MdButtonModule, MdInputModule, MdCardModule } from '@angular/material';
import { PostServiceApi } from '../apis';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MdInputModule,
    MdCardModule,
    MdButtonModule
  ],
  declarations: [PostComponent],
  exports: [PostComponent],
  providers: [
    PostServiceApi
  ]
})
export class PostModule { }
