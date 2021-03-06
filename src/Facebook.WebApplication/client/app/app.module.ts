import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HomeModule } from './home/home.module';
import { RegisterModule } from './register/register.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutes } from './app.routes';
import { AppComponent } from './app.component';
import { MaterialModule } from './core/material.module';
import { LoginModule } from './login/login.module';
import { CoreModule } from './core/core.module';
import { MyProfileModule } from './my-profile/my-profile.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    AppRoutes,
    CoreModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpModule,
    HomeModule,
    RegisterModule,
    LoginModule,
    MyProfileModule,
    MaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

