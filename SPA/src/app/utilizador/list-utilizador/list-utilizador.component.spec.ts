import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListUtilizadorComponent } from './list-utilizador.component';

describe('ListUtilizadorComponent', () => {
  let component: ListUtilizadorComponent;
  let fixture: ComponentFixture<ListUtilizadorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListUtilizadorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListUtilizadorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
