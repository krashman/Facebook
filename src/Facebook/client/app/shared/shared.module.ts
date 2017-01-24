import { RouterModule } from '@angular/router';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({

    imports: [
        CommonModule,
        RouterModule
    ],

    declarations: [

    ],

    exports: [

    ]
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