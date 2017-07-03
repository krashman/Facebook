import { TestBed, inject } from '@angular/core/testing';

import { ProfileServiceApi } from './profile.service.api';

describe('ProfileService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProfileServiceApi]
    });
  });

  it('should ...', inject([ProfileServiceApi], (service: ProfileServiceApi) => {
    expect(service).toBeTruthy();
  }));
});
