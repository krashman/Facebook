import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  email: string;
  password: string;
  
  constructor(private fb: FormBuilder) {
    this.form = this.form = fb.group({
      'email': this.email,
      'password': this.password
    });
  }

  ngOnInit() {
  }

}