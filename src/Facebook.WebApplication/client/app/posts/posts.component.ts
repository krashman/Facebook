import { Component, OnInit, Input } from '@angular/core';
import { PostServiceApi, Post } from '../apis';
import { Observable } from 'rxjs/Observable';
import * as _ from 'lodash';

@Component({
  selector: 'posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit {
  @Input() userId;
  posts: Array<Post>;
  constructor(private postServiceApi: PostServiceApi) { }

  ngOnInit() {
    let request: Observable<Post[]>;
    request = this.userId ?
      this.postServiceApi.getAllMyPosts(this.userId) : this.postServiceApi.getAllPosts();

    request.subscribe(x => {
      this.posts = x;
      console.log(x);
    });
  }


}
