import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { SharedModule } from './shared/shared.module';
import { HomeModule } from './home/home.module';
import { AboutModule } from './+about/about.module';
import { AppRoutes } from './app.routes';
import { MdlModule } from 'angular2-mdl';
import { AppComponent } from './app.component';

@NgModule({
    imports: [
        BrowserModule,
        AppRoutes,
        MdlModule,
        SharedModule.forRoot(),
        HomeModule,
        AboutModule
    ],

    declarations: [
        AppComponent
    ],

    bootstrap: [AppComponent],
})

export class AppModule { }