import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MangaChaptersDetailComponent } from './manga-chapters-detail.component';

describe('MangaChaptersDetailComponent', () => {
  let component: MangaChaptersDetailComponent;
  let fixture: ComponentFixture<MangaChaptersDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MangaChaptersDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MangaChaptersDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
