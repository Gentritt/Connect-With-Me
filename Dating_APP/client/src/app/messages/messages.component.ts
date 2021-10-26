import { Component, OnInit } from '@angular/core';
import { Message } from '../models/message';
import { MessageParams } from '../models/messageParams';
import { Pagination } from '../models/pagination';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages: Partial<Message[]>;
  pagination!: Pagination;
  messageParams!: MessageParams
  container = 'Inbox';
  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
    this.loadMessages();
    
  }

  loadMessages(){
    this.messageService.getMessages(this.container).subscribe(response=> {
    this.messages = response;
   })
  }

  // pageChanged(event: any){
  //   this.messageParams.pageNumber = event.page;
  //   this.loadMessages();
  // }

}
