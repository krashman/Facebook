import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './core';
import { Subject } from 'rxjs/Subject';
import { Router } from '@angular/router';
import 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

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
    this.authenticationService.isSignedIn().takeUntil(this.unsubscribe).subscribe(isSignedIn => {
      this.loginButtonText = isSignedIn ? 'Logout' : 'Login';
      console.log(this.loginButtonText);
    });

    // Optional strategy for refresh token through a scheduler.
    this.authenticationService.startupTokenRefresh();
  }

  title = 'app works!';
}
