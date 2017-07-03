import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MyProfileComponent } from './my-profile.component';
import { AuthenticationGuard } from '../core/services/authentication.guard';

const routes: Routes = [
  { path: 'my-profile', component: MyProfileComponent, canActivate: [AuthenticationGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyProfileRoutingModule { }
