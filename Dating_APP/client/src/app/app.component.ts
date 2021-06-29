import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { User } from './modals/user';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  users: any;

    constructor(private http: HttpClient, private accountservice: AccountService) { }

  ngOnInit() {
      this.getUsers();
      this.setCurrentUser();
    }

    setCurrentUser() {
        const user: User = JSON.parse(localStorage.getItem('user')!); //saving the current user in localstorage
        this.accountservice.setCurrentUser(user); //setting user in our account service
    }

  getUsers() {
    this.http.get('https://localhost:44361/api/users').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    })
  }

}
