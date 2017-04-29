import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './core';
import { Subject } from 'rxjs/Subject';
import { Router } from '@angular/router';
import 'rxjs';

import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  name: Observable<string>;
  signedIn: Observable<boolean>;
  unsubscribe: Subject<void>;

  constructor(private authenticationService: AuthenticationService, private router: Router) {

  }

  loginButtonText: string;

  sign() {
    if (this.authenticationService.isSignedIn()) {
      this.authenticationService.signOut();
      this.router.navigate(['/login']);
    }
    else {

    }
  }

  ngOnInit() {
    this.unsubscribe = new Subject<void>();
    this.signedIn = this.authenticationService.isSignedIn();
    this.signedIn.takeUntil(this.unsubscribe).subscribe(isSignedIn => {
      this.loginButtonText = isSignedIn ? 'Logout' : 'Login';
      console.log(this.loginButtonText);
    });

    this.name = this.authenticationService.getUser()
      .map((user: any) => (typeof user.given_name !== 'undefined') ? user.given_name : null);

    // Optional strategy for refresh token through a scheduler.
    this.authenticationService.startupTokenRefresh();
  }

  title = 'app works!';
}
