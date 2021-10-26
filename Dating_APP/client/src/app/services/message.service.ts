import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Message } from '../models/message';
import { MessageParams } from '../models/messageParams';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = 'https://localhost:44361/api/';

  constructor(private http: HttpClient) {

    
  }
  getMessages(container: string){
    return this.http.get<Partial<Message[]>>(this.baseUrl + 'messages?container=' + container);
  }

  getMessageThread(username:string){
    return this.http.get<Message[]>(this.baseUrl + 'messages/thread/' + username);
  }

  sendMessage(username: string, content: string){
    return this.http.post<Message>(this.baseUrl + 'messages', {recipientUsername: username, content})
  }
  deleteMessage(id:number){
    return this.http.delete(this.baseUrl + 'messages/'+id);
  }
}
