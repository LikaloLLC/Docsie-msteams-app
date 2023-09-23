import { TestBed } from '@angular/core/testing';

import { ScriptRendererService } from './script-renderer.service';

describe('ScriptRendererService', () => {
  let service: ScriptRendererService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScriptRendererService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
