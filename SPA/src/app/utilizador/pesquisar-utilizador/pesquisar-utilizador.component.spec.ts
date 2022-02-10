import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PesquisarUtilizadorComponent } from './pesquisar-utilizador.component';

describe('PesquisarUtilizadorComponent', () => {
  let component: PesquisarUtilizadorComponent;
  let fixture: ComponentFixture<PesquisarUtilizadorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PesquisarUtilizadorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PesquisarUtilizadorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
