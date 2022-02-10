import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarRelacaoComponent } from './editar-relacao.component';

describe('EditarRelacaoComponent', () => {
  let component: EditarRelacaoComponent;
  let fixture: ComponentFixture<EditarRelacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditarRelacaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditarRelacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
