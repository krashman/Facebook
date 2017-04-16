import { RouterModule } from '@angular/router';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './navigation.component';
import { UserService } from './index';

import {
    MdButtonModule,
    MdSnackBarModule,
    MdCardModule,
    MdInputModule,
    MdCheckboxModule,
    MdIconModule,
    MdSidenavModule,
    MdSelectModule,
    MdToolbarModule,
    MdListModule,
    MdProgressBarModule,
    MdProgressSpinnerModule
} from '@angular/material';
@NgModule({

    imports: [
        CommonModule,
        RouterModule,
        MdButtonModule,
        MdSnackBarModule,
        MdCardModule,
        MdInputModule,
        MdCheckboxModule,
        MdIconModule,
        MdSidenavModule,
        MdSelectModule,
        MdToolbarModule,
        MdListModule,
        MdProgressBarModule,
        MdProgressSpinnerModule
    ],

    declarations: [
        NavigationComponent
    ],

    exports: [
        NavigationComponent,
        MdButtonModule,
        MdSnackBarModule,
        MdCardModule,
        MdInputModule,
        MdCheckboxModule,
        MdIconModule,
        MdSidenavModule,
        MdSelectModule,
        MdToolbarModule,
        MdListModule,
        MdProgressBarModule,
        MdProgressSpinnerModule
    ],

    providers: [UserService]
})

export class SharedModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: SharedModule,
            providers: [
            ]
        };
    }
}