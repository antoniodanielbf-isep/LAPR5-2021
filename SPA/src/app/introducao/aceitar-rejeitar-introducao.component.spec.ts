import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AceitarRejeitarIntroducaoComponent } from './aceitar-rejeitar-introducao.component';

describe('AceitarRejeitarIntroducaoComponent', () => {
  let component: AceitarRejeitarIntroducaoComponent;
  let fixture: ComponentFixture<AceitarRejeitarIntroducaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AceitarRejeitarIntroducaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AceitarRejeitarIntroducaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
