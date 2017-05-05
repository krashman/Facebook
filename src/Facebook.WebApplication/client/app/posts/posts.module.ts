import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostsComponent } from './posts.component';
import { MdButtonModule, MdIconModule, MdInputModule, MdCardModule } from '@angular/material';
import { CommentModule } from '../comment/comment.module';
@NgModule({
  imports: [
    CommonModule,
    MdInputModule,
    MdCardModule,
    MdButtonModule,
    MdIconModule,
    CommentModule
  ],
  declarations: [PostsComponent],
  exports: [PostsComponent]
})
export class PostsModule { }
