import { RouterModule } from '@angular/router';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './navigation.component';
import { UserService } from './index';
@NgModule({

    imports: [
        CommonModule,
        RouterModule
    ],

    declarations: [
        NavigationComponent
    ],

    exports: [
        NavigationComponent
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