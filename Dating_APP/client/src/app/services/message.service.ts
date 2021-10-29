import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Message } from '../models/message';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = 'https://localhost:44361/api/';
  hubUrl = environment.hubsUrl;
  private hubConnection: HubConnection;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

  constructor(private http: HttpClient) {  
  }
  
  createHubConnection( user: User, otherUsername: string){
    this.hubConnection = new HubConnectionBuilder()
    .withUrl(this.hubUrl + 'message?user=' + otherUsername, {
      accessTokenFactory: ()=> user.token
    })
    .withAutomaticReconnect()
    .build()

    this.hubConnection.start().catch(error=> console.log(error));
    this.hubConnection.on('ReceiveMessageThread', messages=> {
      this.messageThreadSource.next(messages);
    })

    this.hubConnection.on('NewMessage', message => {
      this.messageThread$.pipe(take(1)).subscribe(messages=> {
        this.messageThreadSource.next([...messages, message]);
      })
    })
  }

  stopHubConnection(){
    if(this.hubConnection){
      this.hubConnection.stop();
    }
   
  }
  getMessages(container: string){
    return this.http.get<Partial<Message[]>>(this.baseUrl + 'messages?container=' + container);
  }

  getMessageThread(username:string){
    return this.http.get<Message[]>(this.baseUrl + 'messages/thread/' + username);
  }

   async sendMessage(username: string, content: string){
    return this.hubConnection.invoke('SendMessage',{recipientUsername: username, content})
    .catch(error=> console.log(error));
  }
  deleteMessage(id:number){
    return this.http.delete(this.baseUrl + 'messages/'+id);
  }
}
