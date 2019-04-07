import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MangaSectionComponent } from './manga-section.component';

describe('MangaSectionComponent', () => {
  let component: MangaSectionComponent;
  let fixture: ComponentFixture<MangaSectionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MangaSectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MangaSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
