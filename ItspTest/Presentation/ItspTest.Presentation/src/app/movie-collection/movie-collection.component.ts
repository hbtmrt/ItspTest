import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { of, Subject, switchMap, takeUntil } from 'rxjs';
import { AccountUIService } from '../account-ui.service';
import { CollectionDataService } from '../collection-data.service';
import { MovieCollection } from './movie-collection.model';

@Component({
  selector: 'app-movie-collection',
  templateUrl: './movie-collection.component.html',
  styleUrls: ['./movie-collection.component.scss']
})
export class MovieCollectionComponent implements OnInit, OnDestroy {

  movieCollections: Array<MovieCollection> = [];
  private userId: string = "";

  private destroy$ = new Subject();

  constructor(
    private accountUIService: AccountUIService,
    private dataService: CollectionDataService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.userId = localStorage.getItem('itspUserId') || "";
    this.loadMovieCollections();
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

  navigateToCollection(id: number) {
    this.router.navigate([id, 'movies']);
  }

  private loadMovieCollections() {
    this.dataService.getMovieCollections()
      .pipe(takeUntil(this.destroy$))
      .subscribe((collections: Array<MovieCollection>) => {
        this.movieCollections = collections;
        this.RefineMovieCollection();
      });
  }

  private RefineMovieCollection() {
    this.movieCollections.forEach(element => {
      element.canEdit = element.userId == this.userId;
    });
  }
}
