import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = 'https://localhost:44361/api/';


  constructor(private http: HttpClient) { }

  getUserWithRoles(){
    return this.http.get<Partial<User[]>>(this.baseUrl + 'admin/users-with-roles');
  }
}
