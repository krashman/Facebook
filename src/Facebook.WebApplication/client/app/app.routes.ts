import { Routes, RouterModule } from '@angular/router';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', redirectTo: 'home' },
    { path: 'register', redirectTo: 'register' },
    { path: 'login', redirectTo: 'login' },
    { path: 'my-profile', redirectTo: 'my-profile' }
];

export const AppRoutes = RouterModule.forRoot(routes);