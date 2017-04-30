import { TestBed, inject } from '@angular/core/testing';

import { PostServiceApi } from './post.service.api';

describe('PostService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PostServiceApi]
    });
  });

  it('should ...', inject([PostServiceApi], (service: PostServiceApi) => {
    expect(service).toBeTruthy();
  }));
});
