import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private accountService: AccountService, private toastrService: ToastrService){

  }
  canActivate(): Observable<boolean>  {
    
    return this.accountService.currentUser$.pipe(
      map(user =>{
        if(user.roles.includes('Admin') || user.roles.includes('Moderator')){
          return true;
        }
        this.toastrService.error("You're not an Admin, please get back !!!");
        return false;
      })  
    )
  }
  
}
