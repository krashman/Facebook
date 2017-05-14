import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SocialInteractionsServiceApi } from '../apis';
import { SocialInteractionsComponent } from './social-interactions.component';
import { MdIconModule, MdButtonModule } from '@angular/material';

@NgModule({
  imports: [
    CommonModule,
    MdIconModule,
    MdButtonModule
  ],
  declarations: [SocialInteractionsComponent],
  exports: [SocialInteractionsComponent],
  providers: [SocialInteractionsServiceApi]
})
export class SocialInteractionsModule { }
