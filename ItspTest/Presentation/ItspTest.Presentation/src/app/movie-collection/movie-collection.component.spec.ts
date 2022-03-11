import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieCollectionComponent } from './movie-collection.component';

describe('MovieCollectionComponent', () => {
  let component: MovieCollectionComponent;
  let fixture: ComponentFixture<MovieCollectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MovieCollectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MovieCollectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
