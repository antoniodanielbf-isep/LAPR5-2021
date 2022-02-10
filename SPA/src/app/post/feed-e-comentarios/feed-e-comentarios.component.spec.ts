import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FeedEComentariosComponent } from './feed-e-comentarios.component';

describe('FeedEComentariosComponent', () => {
  let component: FeedEComentariosComponent;
  let fixture: ComponentFixture<FeedEComentariosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FeedEComentariosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FeedEComentariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
