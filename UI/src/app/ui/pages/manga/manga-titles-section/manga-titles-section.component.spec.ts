import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MangaTitlesSectionComponent } from './manga-titles-section.component';

describe('MangaTitlesSectionComponent', () => {
  let component: MangaTitlesSectionComponent;
  let fixture: ComponentFixture<MangaTitlesSectionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MangaTitlesSectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MangaTitlesSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
