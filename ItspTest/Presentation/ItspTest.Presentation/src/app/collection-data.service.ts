import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
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
}
