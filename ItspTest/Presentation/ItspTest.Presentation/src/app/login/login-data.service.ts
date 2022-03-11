import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginRequest, LoginResponse } from './login.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginDataService {

  private baseUrl = "http://localhost:61005";

  private headers= new HttpHeaders()
      .set('content-type', 'application/json')
      .set('Access-Control-Allow-Origin', '*');

  constructor(private http: HttpClient) { }

  login(username: string, password: string) : Observable<LoginResponse>
  {
      const url  = this.baseUrl + "/api/Account/authenticate";
      var request: LoginRequest = {
        username: username,
        password: password
      };

      return this.http.post<LoginResponse>(url, request, {headers: this.headers});
  }
}
