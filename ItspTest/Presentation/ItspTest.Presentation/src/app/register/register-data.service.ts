import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { last, Observable } from 'rxjs';
import { RegisterRequest } from './register.model';

@Injectable({
  providedIn: 'root'
})
export class RegisterDataService {
  private baseUrl = "http://localhost:61005";

  private headers = new HttpHeaders()
    .set('content-type', 'application/json')
    .set('Access-Control-Allow-Origin', '*');

  constructor(private http: HttpClient) { }

  register(username: string, password: string, firstName: string, lastName: string): Observable<any> {
    const url = this.baseUrl + "/api/Account/register";
    var request: RegisterRequest = {
      username: username,
      password: password,
      firstName: firstName,
      lastName: lastName
    };

    return this.http.post<any>(url, request, { headers: this.headers });
  }
}
