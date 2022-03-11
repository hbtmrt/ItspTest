import { TestBed } from '@angular/core/testing';

import { AccountUIService } from './account-ui.service';

describe('AccountUiService', () => {
  let service: AccountUIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccountUIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
