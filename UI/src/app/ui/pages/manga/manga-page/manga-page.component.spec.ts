import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MangaPageComponent } from './manga-page.component';

describe('MangaPageComponent', () => {
  let component: MangaPageComponent;
  let fixture: ComponentFixture<MangaPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MangaPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MangaPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
