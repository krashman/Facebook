import { NgModule } from '@angular/core';
import {
  MdButtonModule,
  MdSnackBarModule,
  MdCardModule,
  MdInputModule,
  MdCheckboxModule,
  MdIconModule,
  MdSidenavModule,
  MdSelectModule,
  MdRadioModule,
  MdToolbarModule,
  MdListModule,
  MdProgressBarModule,
  MdProgressSpinnerModule } from '@angular/material';


@NgModule({
    imports: [
      MdButtonModule,
      MdSnackBarModule,
      MdCardModule,
      MdInputModule,
      MdCheckboxModule,
      MdIconModule,
      MdSidenavModule,
      MdSelectModule,
      MdRadioModule,
      MdToolbarModule,
      MdListModule,
      MdProgressBarModule,
      MdProgressSpinnerModule ],
    exports: [
      MdButtonModule,
      MdSnackBarModule,
      MdCardModule,
      MdInputModule,
      MdCheckboxModule,
      MdIconModule,
      MdSidenavModule,
      MdSelectModule,
      MdRadioModule,
      MdToolbarModule,
      MdListModule,
      MdProgressBarModule,
      MdProgressSpinnerModule ]
})
export class MaterialModule {  }