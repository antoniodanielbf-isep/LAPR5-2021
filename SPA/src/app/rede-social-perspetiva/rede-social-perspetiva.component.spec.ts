import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RedeSocialPerspetivaComponent } from './rede-social-perspetiva.component';

describe('RedeSocialPerspetivaComponent', () => {
  let component: RedeSocialPerspetivaComponent;
  let fixture: ComponentFixture<RedeSocialPerspetivaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RedeSocialPerspetivaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RedeSocialPerspetivaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
