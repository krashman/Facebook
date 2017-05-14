import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostServiceApi } from './post.service.api';
import { SocialInteractionsServiceApi } from './social-interactions.service.api';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [],
  providers: [PostServiceApi, SocialInteractionsServiceApi]
})
export class ApisModule { }
