import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MangaSourcesSectionComponent } from './manga-sources-section.component';

describe('MangaSourcesSectionComponent', () => {
  let component: MangaSourcesSectionComponent;
  let fixture: ComponentFixture<MangaSourcesSectionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MangaSourcesSectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MangaSourcesSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
