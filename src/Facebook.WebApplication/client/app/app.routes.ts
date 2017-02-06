import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'home', component: HomeComponent, pathMatch: 'full' },
    { path: 'register', redirectTo: 'register' },
    { path: 'about', loadChildren: './+about/about.module#AboutModule' }
];

export const AppRoutes = RouterModule.forRoot(routes);