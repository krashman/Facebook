import { Component, OnInit, Input } from '@angular/core';
import { SocialInteractionsServiceApi, SocialInteractions } from '../apis';

@Component({
  selector: 'social-interactions',
  templateUrl: './social-interactions.component.html',
  styleUrls: ['./social-interactions.component.scss']
})
export class SocialInteractionsComponent implements OnInit {
  socialInteraction: SocialInteractions;
  @Input() postId;

  constructor(private socialInteractionsServiceApi: SocialInteractionsServiceApi) { }

  ngOnInit() {
    this.socialInteractionsServiceApi.getSocialInteraction(this.postId).subscribe(x => this.socialInteraction = x);
  }

}
