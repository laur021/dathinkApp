import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  // variables
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5001/api/';

  //ctor
  constructor() {}

  // Make sure to return the observable from http.post
  login(model: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'account/login', model);
  }
}
