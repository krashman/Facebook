import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostsComponent } from './posts.component';
import { MdButtonModule, MdIconModule, MdInputModule, MdCardModule } from '@angular/material';
import { PostModule } from '../post/post.module';
@NgModule({
  imports: [
    CommonModule,
    MdInputModule,
    MdCardModule,
    MdButtonModule,
    MdIconModule,
    PostModule
  ],
  declarations: [PostsComponent],
  exports: [PostsComponent]
})
export class PostsModule { }
