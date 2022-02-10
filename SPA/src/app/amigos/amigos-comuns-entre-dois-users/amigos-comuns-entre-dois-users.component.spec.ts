import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AmigosComunsEntreDoisUsersComponent } from './amigos-comuns-entre-dois-users.component';

describe('AmigosComunsEntreDoisUsersComponent', () => {
  let component: AmigosComunsEntreDoisUsersComponent;
  let fixture: ComponentFixture<AmigosComunsEntreDoisUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AmigosComunsEntreDoisUsersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AmigosComunsEntreDoisUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
