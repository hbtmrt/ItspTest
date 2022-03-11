import { TestBed } from '@angular/core/testing';

import { LoginData.Service.TsService } from './login-data.service.ts.service';

describe('LoginData.Service.TsService', () => {
  let service: LoginData.Service.TsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoginData.Service.TsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
