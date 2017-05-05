import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommentComponent } from './comment.component';
import { PostServiceApi } from '../apis';import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MdButtonModule, MdIconModule, MdInputModule } from '@angular/material';
@NgModule({
  imports: [
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    MdButtonModule,
    MdIconModule,
    MdInputModule
  ],
  declarations: [CommentComponent],
  exports: [CommentComponent],
  providers: [PostServiceApi]
})
export class CommentModule { }
