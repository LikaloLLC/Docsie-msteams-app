import { TestBed } from '@angular/core/testing';

import { DocsieService } from './docsie.service';

describe('DocsieService', () => {
  let service: DocsieService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DocsieService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
