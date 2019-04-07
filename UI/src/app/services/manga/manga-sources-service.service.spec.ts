import { TestBed } from '@angular/core/testing';

import { MangaSourcesServiceService } from './manga-sources-service.service';

describe('MangaSourcesServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MangaSourcesServiceService = TestBed.get(MangaSourcesServiceService);
    expect(service).toBeTruthy();
  });
});
