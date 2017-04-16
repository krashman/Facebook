import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';
import { UserService, User } from '../shared';
const emailValidator = Validators.pattern('^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$');

@Component({
    selector: 'register',
    templateUrl: 'register.component.html',
    styleUrls: ['./register.component.scss']
})

export class RegisterComponent implements OnInit {
    public form: FormGroup;
    public firstName = new FormControl('');
    public lastName = new FormControl('');
    public birthday = new FormControl('');
    public email = new FormControl('', emailValidator);
    public password = new FormControl('');

    constructor(private router: Router, private fb: FormBuilder, private userService: UserService) {
        this.form = fb.group({
            'firstName': this.firstName,
            'lastName': this.lastName,
            'email': this.email,
            'password': this.password,
            'birthday': this.birthday
        });
    }

    ngOnInit() {

    }

    errorMessages;

    public onSubmit() {
        console.log(this.form);
        let model: User = { firstName: this.firstName.value, lastName: this.lastName.value, password: this.password.value, email: this.email.value, birthday: this.birthday.value };
        this.userService.create(model)
            .subscribe(
            res => {
                if (res.succeeded) {
                    this.router.navigate(["/"]);
                    console.log('success');
                }
                else {
                    this.errorMessages = res.errors;
                }
                // this.alertService.success('Registration successful', true);
                // this.router.navigate(['/login']);
            },
            error => {
                let errMsg = (error.message) ? error.message :
                    error.status ? `${error.status} - ${error.statusText}` : 'Server error';
                console.log(errMsg);
                this.errorMessages = "Server error. Try later.";
            });
    }

    public onDisableForm(formDisabled: boolean) {
        if (formDisabled) {
            this.form.disable();
        } else {
            this.form.enable();
        }
    }

}