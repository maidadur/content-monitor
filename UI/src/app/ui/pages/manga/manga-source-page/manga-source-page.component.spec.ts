import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MangaSourcePageComponent } from './manga-source-page.component';

describe('MangaSourcePageComponent', () => {
  let component: MangaSourcePageComponent;
  let fixture: ComponentFixture<MangaSourcePageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MangaSourcePageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MangaSourcePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
