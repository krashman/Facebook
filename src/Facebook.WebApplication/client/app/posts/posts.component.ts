import { Component, OnInit } from '@angular/core';
import { PostServiceApi, Post } from '../apis';

@Component({
  selector: 'posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit {
  posts: Array<Post>;
  constructor(private postServiceApi: PostServiceApi) { }

  ngOnInit() {
    this.postServiceApi.getAllPosts().subscribe(x => {
      this.posts = x;
      console.log(x);
    });
  }

}
