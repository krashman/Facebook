import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { AboutRoutes } from './about.routes';
import { AboutComponent } from './about.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        HttpModule,
        AboutRoutes
    ],

    declarations: [
        AboutComponent
    ],

    exports: [
        AboutComponent
    ]
})

export class AboutModule { }