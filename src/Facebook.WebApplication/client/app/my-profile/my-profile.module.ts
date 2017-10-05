import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyProfileRoutingModule } from './my-profile-routing.module';
import { MyProfileComponent } from './my-profile.component';
import { CoreModule } from '../core/core.module';
import { ProfileServiceApi } from '../apis';
import { MdInputModule, MdButtonModule } from '@angular/material';
import { FormsModule } from '@angular/forms';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MdButtonModule,
    MdInputModule,
    MyProfileRoutingModule,
    CoreModule
  ],
  declarations: [MyProfileComponent],
  providers: [ProfileServiceApi]
})
export class MyProfileModule { }
