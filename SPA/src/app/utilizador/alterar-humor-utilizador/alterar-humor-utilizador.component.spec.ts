import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlterarHumorUtilizadorComponent } from './alterar-humor-utilizador.component';

describe('AlterarHumorUtilizadorComponent', () => {
  let component: AlterarHumorUtilizadorComponent;
  let fixture: ComponentFixture<AlterarHumorUtilizadorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AlterarHumorUtilizadorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AlterarHumorUtilizadorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
