import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PedirIntroducaoComponent } from './pedir-introducao.component';

describe('PedirIntroducaoComponent', () => {
  let component: PedirIntroducaoComponent;
  let fixture: ComponentFixture<PedirIntroducaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PedirIntroducaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PedirIntroducaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
