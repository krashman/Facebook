import { Routes, RouterModule } from '@angular/router';

import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', redirectTo: 'home' },
    { path: 'register', redirectTo: 'register' },
    { path: 'login', redirectTo: 'login' },
    // { path: 'register', component: RegisterComponent }
];

export const AppRoutes = RouterModule.forRoot(routes);