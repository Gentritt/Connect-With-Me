import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';

@Component({

  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
    model: any = {};

    constructor(public accountservice: AccountService, private router: Router, private toastr: ToastrService, private fb: FormBuilder) { }
    ngOnInit(): void {
  }


  login() {
      this.accountservice.login(this.model).subscribe(response => {
          this.router.navigateByUrl('/members');
          this.toastr.success('Successfully Logged')
    })
  }
    logout() {
        this.accountservice.logout();
        this.router.navigateByUrl('/');
    }
}
