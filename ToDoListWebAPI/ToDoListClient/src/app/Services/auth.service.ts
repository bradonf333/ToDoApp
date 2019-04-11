import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { config } from '../app.config';
import { User } from '../Models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserBS: BehaviorSubject<User>;
  public currentUser: Observable<any>;

  jwtHelper = new JwtHelperService();
  constructor(private http: HttpClient) {
    this.currentUserBS = new BehaviorSubject<User>(
      JSON.parse(localStorage.getItem(config.userKey))
    );
    this.currentUser = this.currentUserBS.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserBS.value;
  }

  login(userId: string, password: string) {
    return this.http
      .post<User>(`${config.todoWebApiUrl}/User/authenticate`, {
        userId,
        password
      })
      .pipe(
        map(user => {
          // login successful if there's a jwt token in the response
          if (user && user.token) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem(config.userKey, JSON.stringify(user));
          }
          console.log(user);
          return user;
        })
      );
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem(config.userKey);
    // Check whether the token is expired and return
    // true or false
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem(config.userKey);
  }
}
