import { TestBed, inject } from '@angular/core/testing';

import { SocialInteractionsServiceApi } from './social-interactions.service.api';

describe('SocialInteractionsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SocialInteractionsServiceApi]
    });
  });

  it('should ...', inject([SocialInteractionsServiceApi], (service: SocialInteractionsServiceApi) => {
    expect(service).toBeTruthy();
  }));
});
