import { Component, OnInit } from '@angular/core';

import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';


const emailValidator = Validators.pattern('^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$');

@Component({
    selector: 'home-component',
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {
    public form: FormGroup;
    public firstName = new FormControl('');
    public lastName = new FormControl('');
    public email = new FormControl('', emailValidator);
    public password = new FormControl('');
    ngOnInit() {

    }
    
    
    constructor(private fb: FormBuilder) {
        this.form = fb.group({
            'firstName': this.firstName,
            'lastName': this.lastName,
            'email': this.email,
            'password': this.password
        });
    }

    public onSubmit() {
        console.log(this.form);
    }

    public onDisableForm(formDisabled: boolean) {
        if (formDisabled) {
            this.form.disable();
        } else {
            this.form.enable();
        }
    }

}