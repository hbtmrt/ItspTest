import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountUIService {

  private loggedInUserId$ = new BehaviorSubject<string>("");
  private token$ = new BehaviorSubject<string>("");
  
  constructor() { }

  getUserId(): Observable<string>
  {
    return this.loggedInUserId$.asObservable();
  }

  setUserId(id: string)
  {
    this.loggedInUserId$.next(id);
  }

  getToken(): Observable<string>
  {
    return this.token$.asObservable();
  }

  setToken(token: string)
  {
    this.token$.next(token);
  }
}
