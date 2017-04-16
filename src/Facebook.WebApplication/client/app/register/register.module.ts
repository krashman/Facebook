import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
// import { RegisterRoutes } from './register.routes';
import { RegisterComponent } from './register.component';

import { MaterialModule } from '../material.module';
@NgModule({
   imports: [
       CommonModule,
       FormsModule,
       ReactiveFormsModule,
       HttpModule,
       MaterialModule
    //    RegisterRoutes
   ],

   declarations: [
       RegisterComponent
   ],

   exports: [
       RegisterComponent
   ]
})

export class RegisterModule { }