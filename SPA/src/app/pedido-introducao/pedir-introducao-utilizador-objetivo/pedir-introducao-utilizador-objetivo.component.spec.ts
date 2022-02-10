import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PedirIntroducaoUtilizadorObjetivoComponent } from './pedir-introducao-utilizador-objetivo.component';

describe('PedirIntroducaoUtilizadorObjetivoComponent', () => {
  let component: PedirIntroducaoUtilizadorObjetivoComponent;
  let fixture: ComponentFixture<PedirIntroducaoUtilizadorObjetivoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PedirIntroducaoUtilizadorObjetivoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PedirIntroducaoUtilizadorObjetivoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
