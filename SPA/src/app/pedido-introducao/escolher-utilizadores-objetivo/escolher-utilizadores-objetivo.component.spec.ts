import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EscolherUtilizadoresObjetivoComponent } from './escolher-utilizadores-objetivo.component';

describe('EscolherUtilizadoresObjetivoComponent', () => {
  let component: EscolherUtilizadoresObjetivoComponent;
  let fixture: ComponentFixture<EscolherUtilizadoresObjetivoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EscolherUtilizadoresObjetivoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EscolherUtilizadoresObjetivoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
