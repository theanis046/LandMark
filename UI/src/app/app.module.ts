import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AgmCoreModule } from '@agm/core';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login/login.component';
import { RegisterComponent } from './components/register/register/register.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { routing } from './app.routing';
import { AuthGuard } from './auth.guard';
import { RequestInterceptor } from './request.interceptor';


import { UserService } from './services/user/user.service';
import {AuthenticationService} from './services/authentication/authentication.service'

@NgModule({
  imports:      [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    routing,
    AgmCoreModule.forRoot({apiKey: 'AIzaSyAA6oETfpYm_1c-pLWktPlDNjD0YOTiYPM'})
  ],
  providers : [
    AuthGuard,
    AuthenticationService,
    UserService,
    {
        provide: HTTP_INTERCEPTORS,
        useClass: RequestInterceptor,
        multi: true
    }
  ],
  declarations: [ AppComponent, HomeComponent, LoginComponent, RegisterComponent ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
