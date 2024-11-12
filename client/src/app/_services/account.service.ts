import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { map } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  // variables
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5001/api/';
  currentUser = signal<User | null>(null);

  //ctor
  constructor() {}


  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user => { //user variable get the response(user details) from API
        if(user){
          localStorage.setItem('user', JSON.stringify(user)); //goes to localstorage as keyvalue pair
          this.currentUser.set(user);
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;
      })
    )
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
