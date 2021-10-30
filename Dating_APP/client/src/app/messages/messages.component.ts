import { Component, OnInit } from '@angular/core';
import { Message } from '../models/message';
import { MessageParams } from '../models/messageParams';
import { Pagination } from '../models/pagination';
import { User } from '../models/user';
import { ConfirmService } from '../services/confirm.service';
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
  user: User;
  container = 'Inbox';
  constructor(private messageService: MessageService, private confirmService: ConfirmService) { }

  ngOnInit(): void {
    this.loadMessages();
    
  }

  loadMessages(){
    this.messageService.getMessages(this.container).subscribe(response=> {
    this.messages = response;
   })
  }
  deleteMessage(id:number){

  
    this.confirmService.confirm('Are you sure you want to delete this message? ', 'Disclaimer: This message can still be seen by the recepient, unless he deletes it!').subscribe((result: any)=> {
      if(result){
        this.messageService.deleteMessage(id).subscribe(()=>{
          this.messages.splice(this.messages.findIndex(m=> m!.id == id),1);
        });
      }
    })
  }
  // pageChanged(event: any){
  //   this.messageParams.pageNumber = event.page;
  //   this.loadMessages();
  // }

}
