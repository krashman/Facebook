import { Routes, RouterModule } from '@angular/router';

import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
    // { path: '', component: HomeComponent },
    // { path: 'home', component: HomeComponent, pathMatch: 'full' },
    // { path: 'register', redirectTo: 'register' },
    
//    { path: 'register', component: RegisterComponent }
];

export const AppRoutes = RouterModule.forRoot(routes);