import { RouterModule } from '@angular/router';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './navigation.component';
import { AuthenticationService } from './index';
import { AuthConfig, AuthHttp } from 'angular2-jwt';
import { Http } from '@angular/http';
import { AuthenticationGuard } from './services/authentication.guard';

//TODO: Move somewhere including http object
// Set tokenGetter to use the same storage in AuthenticationService.Helpers.
export function getAuthHttp(http: Http) {
    return new AuthHttp(new AuthConfig({
        noJwtError: true,
        tokenGetter: (() => localStorage.getItem('id_token'))
    }), http);
}

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

    providers: [
        AuthenticationGuard,
        AuthHttp,
        AuthenticationService,
        {
            provide: AuthHttp,
            useFactory: getAuthHttp,
            deps: [Http],
        }]
})

export class CoreModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: CoreModule,
            providers: [
            ]
        };
    }
}