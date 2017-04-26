import { TestBed, inject } from '@angular/core/testing';

import { AuthenticationGuard } from './authentication.guard';

describe('AuthService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthenticationGuard]
    });
  });

  it('should ...', inject([AuthenticationGuard], (service: AuthenticationGuard) => {
    expect(service).toBeTruthy();
  }));
});
