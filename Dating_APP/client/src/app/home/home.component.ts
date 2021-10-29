import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  user: User;
    registerMode = false;
    constructor(public accountService:AccountService ) { 
      this.accountService.currentUser$.pipe(take(1)).subscribe(user=> this.user);

    }

    ngOnInit(): void {
  }

    registerToggle() {
        this.registerMode = !this.registerMode;
    }
    cancelRegisterMode(event: boolean) {
        this.registerMode = event;
    }
}
