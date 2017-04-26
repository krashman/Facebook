import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';
import { AuthenticationGuard } from '../core/services/authentication.guard';


const routes: Routes = [
  { path: 'home', component: HomeComponent, canActivate: [AuthenticationGuard] }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
