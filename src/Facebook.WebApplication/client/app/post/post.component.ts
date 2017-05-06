import { Component, OnInit, Input } from '@angular/core';
import { PostServiceApi } from '../apis';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
@Component({
  selector: 'post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {
  @Input('post') post: any;
  commentFormGroup: FormGroup
  postFormControl = new FormControl('');
  comments: any;
  constructor(private fb: FormBuilder, private postServiceApi: PostServiceApi) { }

  ngOnInit() {
    this.commentFormGroup = this.fb.group({
      'post': this.postFormControl
    });
    this.postServiceApi.getAllPostsWithHttpInfo(this.post.id).subscribe(x => this.comments = x.json());
  }

  postComment() {
    this.postServiceApi.addPost({ content: this.postFormControl.value, parentId: this.post.id }).subscribe();
  }

  deletePost(postId: string) {
    this.postServiceApi.deletePost(postId).subscribe(x => {
      console.log(x);
    });
  }

}
