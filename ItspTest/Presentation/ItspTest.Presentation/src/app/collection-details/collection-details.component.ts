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

  collectionName: string ="";
  collectionId: number = 0;
  searchText: string = "";
  movies: Array<Movie> = [];

  private destroy$ = new Subject();

  constructor(
    private dataService: CollectionDataService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.readCollectionIdAndLoadMovies();
  }

  search(searchText: string) {
    this.searchText = searchText
    this.dataService.searchCollection(this.collectionId, this.searchText)
      .pipe(takeUntil(this.destroy$))
      .subscribe((movies: Array<Movie>) => {
        this.movies = movies;
      });
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
                return this.dataService.searchCollection(this.collectionId, this.searchText);
              })
            )
        })
      )
      .subscribe((movies: Array<Movie>) => {
        this.movies = movies;
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}
