import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObterPedidosLigacaoPendentesComponent } from './obter-pedidos-ligacao-pendentes.component';

describe('ObterPedidosLigacaoPendentesComponent', () => {
  let component: ObterPedidosLigacaoPendentesComponent;
  let fixture: ComponentFixture<ObterPedidosLigacaoPendentesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ObterPedidosLigacaoPendentesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ObterPedidosLigacaoPendentesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
