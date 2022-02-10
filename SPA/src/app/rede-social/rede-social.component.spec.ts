import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RedeSocialComponent } from './rede-social.component';

describe('RedeSocialComponent', () => {
  let component: RedeSocialComponent;
  let fixture: ComponentFixture<RedeSocialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RedeSocialComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RedeSocialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
