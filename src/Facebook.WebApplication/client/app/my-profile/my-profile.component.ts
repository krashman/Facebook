import { Component, OnInit } from '@angular/core';
import { ProfileServiceApi } from '../apis';
import { AuthenticationService } from '../core';
@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.scss']
})
export class MyProfileComponent implements OnInit {
  firstName: string;
  lastName: string;
  myProfile: any;

  constructor(private profileServiceApi: ProfileServiceApi, public authenticationService: AuthenticationService) {
  }

  ngOnInit() {
    this.myProfile = this.profileServiceApi.getMyProfile();
  }

  save() {
    // TODO: Create class for request body
    this.profileServiceApi.updateProfile(
      { "given_name": this.firstName, "family_name": this.lastName }
    ).subscribe(() => {
      this.authenticationService.getUserInfo();
    });
  }

  addUpload(event) {
    this.profileServiceApi.uploadProfilePicture(event)
      .subscribe(x => this.myProfile = this.profileServiceApi.getMyProfile());
    console.log(event);
  }

}
