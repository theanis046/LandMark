import { TestBed, inject } from '@angular/core/testing';

import { LandmarkService } from './landmark.service';

describe('LandmarkService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LandmarkService]
    });
  });

  it('should be created', inject([LandmarkService], (service: LandmarkService) => {
    expect(service).toBeTruthy();
  }));
});
