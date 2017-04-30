import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostServiceApi } from './post.service.api';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [],
  providers: [PostServiceApi]
})
export class ApisModule { }
