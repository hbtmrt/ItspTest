import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from './collection-details/collection-details.model';
import { MovieCollection } from './movie-collection/movie-collection.model';

@Injectable({
  providedIn: 'root'
})
export class CollectionDataService {
  private baseUrl = "http://localhost:61005";

  headers = new HttpHeaders()
    .set('content-type', 'application/json')
    .set('Access-Control-Allow-Origin', '*')
    .set('Authorization', 'Bearer ' + localStorage.getItem("itspToken"));

  constructor(private http: HttpClient) { }

  public getMovieCollections(): Observable<Array<MovieCollection>> {
    const url = this.baseUrl + "/api/Collection";
    return this.http.get<Array<MovieCollection>>(url, { headers: this.headers });
  }

  public getCollection(id: number): Observable<MovieCollection> {
    const url = this.baseUrl + `/api/Collection/${id}`;
    return this.http.get<MovieCollection>(url, { headers: this.headers });
  }

  public searchCollection(collectionId: number, searchText: string): Observable<Array<Movie>> {
    const url = this.baseUrl + `/api/Collection/${collectionId}/movies?search=${searchText}`;
    return this.http.get<Array<Movie>>(url, { headers: this.headers });
  }
}
