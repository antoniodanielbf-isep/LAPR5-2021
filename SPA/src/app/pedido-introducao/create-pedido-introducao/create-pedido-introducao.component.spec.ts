import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePedidoIntroducaoComponent } from './create-pedido-introducao.component';

describe('CreatePedidoIntroducaoComponent', () => {
  let component: CreatePedidoIntroducaoComponent;
  let fixture: ComponentFixture<CreatePedidoIntroducaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreatePedidoIntroducaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatePedidoIntroducaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
