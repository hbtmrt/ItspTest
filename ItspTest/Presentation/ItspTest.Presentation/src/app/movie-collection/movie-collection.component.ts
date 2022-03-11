import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { of, Subject, switchMap, takeUntil } from 'rxjs';
import { AccountUIService } from '../account-ui.service';
import { CollectionDataService } from '../collection-data.service';
import { MovieCollection } from './movie-collection.model';

@Component({
  selector: 'app-movie-collection',
  templateUrl: './movie-collection.component.html',
  styleUrls: ['./movie-collection.component.scss']
})
export class MovieCollectionComponent implements OnInit {

  movieCollections: Array<MovieCollection> = [];
  private userId: string = "";

  private destroy$ = new Subject();

  constructor(
    private accountUIService: AccountUIService,
    private dataService: CollectionDataService
  ) { }

  ngOnInit(): void {
    this.userId = localStorage.getItem('itspUserId') || "";
    this.loadMovieCollections();
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
