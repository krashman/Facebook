import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RegisterRoutingModule } from './register-routing.module';
import { RegisterComponent } from './register.component';
import { MaterialModule } from '../core/material.module';
@NgModule({
   imports: [
       CommonModule,
       FormsModule,
       ReactiveFormsModule,
       HttpModule,
       MaterialModule,
       RegisterRoutingModule
   ],

   declarations: [
       RegisterComponent
   ],

   exports: [
       RegisterComponent
   ]
})

export class RegisterModule { }