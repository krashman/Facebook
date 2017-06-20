import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyProfileRoutingModule } from './my-profile-routing.module';
import { MyProfileComponent } from './my-profile.component';
import { CoreModule } from '../core/core.module';
@NgModule({
  imports: [
    CommonModule,
    MyProfileRoutingModule,
    CoreModule
  ],
  declarations: [MyProfileComponent]
})
export class MyProfileModule { }
