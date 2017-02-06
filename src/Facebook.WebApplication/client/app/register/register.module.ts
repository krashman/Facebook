import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RegisterRoutes } from './register.routes';
import { RegisterComponent } from './register.component';
import { MdlModule } from 'angular2-mdl';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        MdlModule,
        RegisterRoutes
    ],

    declarations: [
        RegisterComponent
    ],

    exports: [
        RegisterComponent
    ]
})

export class RegisterModule { }