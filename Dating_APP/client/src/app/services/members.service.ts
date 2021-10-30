 import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Member } from '../models/member';
import { PaginatedResult } from '../models/pagination';
import { UserParams } from '../models/userParams';
import { getPaginationHeaders,getPaginatedResult } from './paginationHelper';



@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = 'https://localhost:44361/api/';
  members: Member[] = [];
  


  constructor(private http: HttpClient) { }

  getMembers(userParams: UserParams) {

    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);
    return getPaginatedResult<Member[]>(this.baseUrl + 'users', params,this.http);
  
  }

  



  getMember(username: string) {
    const member = this.members.find(x => x.username === username);
    if (member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + username)
  }
  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    )
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }

  addLike(username: string) {
    return this.http.post(this.baseUrl + 'likes/' + username, {});
  }

  getLikes(predicate: string) {
    return this.http.get<Partial<Member[]>>(this.baseUrl + 'likes?predicate=' + predicate);
  }

}
