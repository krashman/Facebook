import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { PostServiceApi } from '../apis';
@Component({
  selector: 'new-post',
  templateUrl: './new.post.component.html',
  styleUrls: ['./new.post.component.scss']
})
export class PostComponent implements OnInit {
  form: FormGroup;
  post = new FormControl('');
  constructor(private fb: FormBuilder, private postServiceApi: PostServiceApi) { }

  ngOnInit() {
    this.form = this.fb.group({
      'post': this.post
    })
  }

  onSubmit() {
    this.postServiceApi.addPost({ content: this.post.value }).subscribe(x => {
      console.log(x);
    });
  }

}
