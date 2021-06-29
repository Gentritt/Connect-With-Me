import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model: any = {}
  loggedIn: boolean | undefined;
  constructor(private accountservice: AccountService) { }

  ngOnInit(): void {
  }

  login() {
    this.accountservice.login(this.model).subscribe(response => {
      console.log(response);
      this.loggedIn = true;
    })
  }
}
