import { Component, OnInit } from '@angular/core';
import { PostServiceApi } from '../apis';
import { FormControl, FormGroup , FormBuilder} from '@angular/forms';
@Component({
  selector: 'comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss']
})
export class CommentComponent implements OnInit {
  commentFormGroup: FormGroup
  post = new FormControl('');
  constructor(private fb: FormBuilder, private postServiceApi: PostServiceApi) { }

  ngOnInit() {
    this.commentFormGroup = this.fb.group({
      'post': this.post
    })
  }

  postComment() {
    this.postServiceApi.addPost({ content: this.post.value, parentId: "111" }).subscribe();
  }

}
