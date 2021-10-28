import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { PresenceService } from './presence.service';
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:44361/api/';

    private currentUserSource = new ReplaySubject<User>(1)
    currentUser$ = this.currentUserSource.asObservable()


    constructor(private http: HttpClient, private presence: PresenceService) { }

    login(model: any) {
        return this.http.post(this.baseUrl + 'account/login', model).pipe(
            map((response: any) => {
        const user = response;
        if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user);
            this.presence.createHubConnection(user);
        }
      })
    )
    }
    register(model: any) {
        return this.http.post(this.baseUrl + 'account/register', model).pipe(
            map((user: any) => {
                if (user) {
                    localStorage.setItem('user', JSON.stringify(user));
                    this.currentUserSource.next(user)
                    this.presence.createHubConnection(user);
                }
                return user;
            })
          
        )
    }
    setCurrentUser(user: User) {
      user.roles = [];
      const roles = this.getDecodedToken(user.token).role;
      Array.isArray(roles) ?  user.roles = roles : user.roles.push(roles);
        this.currentUserSource.next(user);
    }
  logout() {
      localStorage.removeItem('user') ;
      this.currentUserSource.next(null!);
      this.presence.stopHubConnection();
  }

  getDecodedToken(token: any){
    return JSON.parse(atob(token.split('.'  )[1]));
    
  }
}
