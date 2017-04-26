import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService, User } from '../core';
const emailValidator = Validators.pattern('^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$');
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  public email = new FormControl('', emailValidator);
  public password = new FormControl('');


  constructor(private router: Router, private fb: FormBuilder, private authenticationService: AuthenticationService) {
    this.form = this.form = fb.group({
      'email': this.email,
      'password': this.password
    });
  }

  ngOnInit() {
  }

  onSubmit() {
    console.log(this.form);
    let model: User = { password: this.password.value, email: this.email.value };
    this.authenticationService.signIn(model.email, model.password).subscribe(res => {
      // Gets the redirect URL from authentication service.
      // If no redirect has been set, uses the default.
      let redirect: string = this.authenticationService.redirectUrl
        ? this.authenticationService.redirectUrl
        : '/home';

      // Redirects the user.
      this.router.navigate([redirect]);
    }, err => {
      console.error(err);
    })
  }

}