import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject, switchMap, takeUntil } from 'rxjs';
import { CollectionDataService } from '../collection-data.service';
import { Movie } from './collection-details.model';

@Component({
  selector: 'app-collection-details',
  templateUrl: './collection-details.component.html',
  styleUrls: ['./collection-details.component.scss']
})
export class CollectionDetailsComponent implements OnInit, OnDestroy {

  collectionName: string = "";
  collectionId: number = 0;
  searchText: string = "";
  movies: Array<Movie> = [];
  addingMoviesMode = false;
  isUserCreatedCollection = false;
  availableMovies: Array<Movie> = [];

  private destroy$ = new Subject();

  constructor(
    private dataService: CollectionDataService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.readCollectionIdAndLoadMovies();
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

  search(searchText: string) {
    this.searchText = searchText
    this.dataService.searchCollection(this.collectionId, this.searchText)
      .pipe(takeUntil(this.destroy$))
      .subscribe((movies: Array<Movie>) => {
        this.movies = movies;
      });
  }

  addMoviesToCollection() {
    const selectedMovies = this.availableMovies.filter(a => a.isSelected);
    const selectedMovieIds = selectedMovies.map(({ id }) => id);

    this.dataService.addMoviesToCollection(this.collectionId, selectedMovieIds)
      .pipe(takeUntil(this.destroy$))
      .subscribe(data => {
        this.refreshPage();
      });
  }

  deleteMovieFromCollection(movieId: number) {
    this.dataService.deleteMovieFromCollection(this.collectionId, movieId)
      .pipe(takeUntil(this.destroy$))
      .subscribe(data => {
        this.refreshPage();
      });
  }

  private refreshPage() {
    this.search("");
    this.loadAvailableMovies();
  }

  private readCollectionIdAndLoadMovies() {
    this.route.params
      .pipe(
        takeUntil(this.destroy$),
        switchMap(params => {
          this.collectionId = params['id'];
          return this.dataService.getCollection(this.collectionId)
            .pipe(
              takeUntil(this.destroy$),
              switchMap(collection => {
                this.collectionName = collection.name;
                this.isUserCreatedCollection = collection.userId == localStorage.getItem('itspUserId');
                return this.dataService.searchCollection(this.collectionId, this.searchText);
              })
            )
        })
      )
      .subscribe((movies: Array<Movie>) => {
        this.movies = movies;
        this.loadAvailableMovies();
      });
  }

  private loadAvailableMovies() {
    this.dataService.getAvailableMovies(this.collectionId)
      .pipe(takeUntil(this.destroy$))
      .subscribe((movies: Array<Movie>) => {
        this.availableMovies = movies;
      });
  }
}
