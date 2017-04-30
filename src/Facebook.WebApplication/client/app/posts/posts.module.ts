import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostsComponent } from './posts.component';
import { MdButtonModule, MdInputModule, MdCardModule } from '@angular/material';
@NgModule({
  imports: [
    CommonModule,
    MdInputModule,
    MdCardModule,
    MdButtonModule
  ],
  declarations: [PostsComponent],
  exports: [PostsComponent]
})
export class PostsModule { }
