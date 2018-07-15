import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from '../app/components/register/register/register.component';
import { HomeComponent } from '../app/components/home/home.component';
import { LoginComponent } from '../app/components/login/login/login.component';
import { AuthGuard } from './auth.guard';

const appRoutes: Routes = [
   { path: '', component: HomeComponent, canActivate: [AuthGuard] }, //default route
   { path: 'login', component: LoginComponent },
   { path: 'register', component: RegisterComponent },
   { path: '**', redirectTo: '' } //fallback route
];

export const routing = RouterModule.forRoot(appRoutes);