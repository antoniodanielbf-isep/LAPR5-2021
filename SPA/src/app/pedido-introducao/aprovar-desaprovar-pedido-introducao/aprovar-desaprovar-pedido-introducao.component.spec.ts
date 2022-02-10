import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AprovarDesaprovarPedidoIntroducaoComponent } from './aprovar-desaprovar-pedido-introducao.component';

describe('AprovarDesaprovarPedidoIntroducaoComponent', () => {
  let component: AprovarDesaprovarPedidoIntroducaoComponent;
  let fixture: ComponentFixture<AprovarDesaprovarPedidoIntroducaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AprovarDesaprovarPedidoIntroducaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AprovarDesaprovarPedidoIntroducaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
