import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostServiceApi } from './post.service.api';
import { SocialInteractionsServiceApi } from './social-interactions.service.api';
import { ProfileServiceApi } from './profile.service.api';
@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [],
  providers: [PostServiceApi, SocialInteractionsServiceApi, ProfileServiceApi]
})
export class ApisModule { }
