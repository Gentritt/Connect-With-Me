import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { User } from './models/user';
import { AccountService } from './services/account.service';
import { PresenceService } from './services/presence.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Connect with me';
  users: any;

    constructor(private accountservice: AccountService, private presence: PresenceService) { }

  ngOnInit() {
      this.setCurrentUser();
    }

    setCurrentUser() {
        const user: User = JSON.parse(localStorage.getItem('user')!); //saving the current user in localstorage

        if(user){
          this.accountservice.setCurrentUser(user);
          this.presence.createHubConnection(user);
        }
         //setting user in our account service
    }

}
