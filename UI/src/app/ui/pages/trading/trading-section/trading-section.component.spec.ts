import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TradingSectionComponent } from './trading-section.component';

describe('TradingSectionComponent', () => {
  let component: TradingSectionComponent;
  let fixture: ComponentFixture<TradingSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TradingSectionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TradingSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
