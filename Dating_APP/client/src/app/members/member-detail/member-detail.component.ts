import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../models/member';
import { MembersService } from '../../services/members.service';
import { NgxGalleryAnimation, NgxGalleryImage } from '@kolkov/ngx-gallery';
import { NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { MessageService } from 'src/app/services/message.service';
import { Message } from 'src/app/models/message';
import { PresenceService } from 'src/app/services/presence.service';
import { AccountService } from 'src/app/services/account.service';
import { User } from 'src/app/models/user';
import { take } from 'rxjs/operators';
@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit, OnDestroy {
@ViewChild ('memberTabs', {static:true}) memberTabs: TabsetComponent;
activeTab: TabDirective;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[] ;
  member: Member;
  messages: Message[] = [];
  user: User;
  constructor(public presence: PresenceService, private route: ActivatedRoute, private messageServicing: MessageService, private accountService: AccountService) {

    this.accountService.currentUser$.pipe(take(1)).subscribe(user=> this.user = user);
   }


  ngOnInit(): void {
    this.route.data.subscribe(data=> {
      this.member = data.member;
    })
    this.route.queryParams.subscribe(params=> {
      params.tab ? this.selectTab(params.tab) : this.selectTab(0);
    })
    this.galleryImages = this.getImages();

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ]
  
  
  }

  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    for (const photo of this.member.photos) {
      imageUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url
      })
    }
    return imageUrls;
  }

  // loadMember() {
  //   this.memberService.getMember(this.route.snapshot.paramMap.get('username') || '{}').subscribe(member => {
  //     this.member = member;
     
  //   })
  // }
  loadMessages(){
    this.messageServicing.getMessageThread(this.member.username).subscribe(messages=> {
      this.messages = messages;
    })
  }

  selectTab(tabId:number){
    this.memberTabs.tabs[tabId].active = true;
  }

  onTabActivated(data: TabDirective){
    this.activeTab = data;
    if(this.activeTab.heading === 'Messages' && this.messages.length === 0){
     this.messageServicing.createHubConnection(this.user, this.member.username);
    }
    else {
      this.messageServicing.stopHubConnection();
    } 
  }
  ngOnDestroy(): void {
    this.messageServicing.stopHubConnection();
  }

}
