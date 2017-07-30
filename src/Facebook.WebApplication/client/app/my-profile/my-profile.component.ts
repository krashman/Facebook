import { Component, OnInit } from '@angular/core';
import { ProfileServiceApi } from '../apis';
@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.scss']
})
export class MyProfileComponent implements OnInit {

  constructor(private profileServiceApi: ProfileServiceApi) { }
  myProfile: any;

  ngOnInit() {
    this.myProfile = this.profileServiceApi.getMyProfile();
  }

  addUpload(event) {
    this.profileServiceApi.uploadProfilePicture(event)
      .subscribe(x => this.myProfile = this.profileServiceApi.getMyProfile());
    console.log(event);
  }

}
