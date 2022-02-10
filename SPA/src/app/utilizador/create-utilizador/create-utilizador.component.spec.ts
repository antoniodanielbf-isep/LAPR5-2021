import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUtilizadorComponent } from './create-utilizador.component';

describe('CreateUtilizadorComponent', () => {
  let component: CreateUtilizadorComponent;
  let fixture: ComponentFixture<CreateUtilizadorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateUtilizadorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateUtilizadorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
