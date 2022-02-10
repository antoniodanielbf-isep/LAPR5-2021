import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeUtilizadorInfoComponent } from './change-utilizador-info.component';

describe('ChangeUtilizadorInfoComponent', () => {
  let component: ChangeUtilizadorInfoComponent;
  let fixture: ComponentFixture<ChangeUtilizadorInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChangeUtilizadorInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeUtilizadorInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
