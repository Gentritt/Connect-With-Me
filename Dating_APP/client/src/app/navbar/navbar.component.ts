import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../modals/user';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
    model: any = {}
    constructor(public accountservice: AccountService, private router: Router, private toastr: ToastrService) { }

    ngOnInit(): void {
  }

  login() {
      this.accountservice.login(this.model).subscribe(response => {
          this.router.navigateByUrl('/members');
          this.toastr.success('Successfully loggedIN')
    }, error => {
          console.log(error);
          this.toastr.error(error.error);
    })
  }
    logout() {
        this.accountservice.logout();
        this.router.navigateByUrl('/');
    }
}
