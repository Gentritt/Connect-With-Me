import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Member } from '../models/member';



@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = 'https://localhost:44361/api/';

  constructor(private http: HttpClient) { }

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users')
  }

  getMember(username:string) {
    return this.http.get<Member>(this.baseUrl + 'users/' + username)
  }
}