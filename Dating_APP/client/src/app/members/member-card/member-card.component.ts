import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { PresenceService } from 'src/app/services/presence.service';
import { Member } from '../../models/member';
import { MembersService } from '../../services/members.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() member: Member | undefined;
  isActive: boolean;
  constructor(public memberService: MembersService, private toastr: ToastrService, public presence: PresenceService
    
    ) { }

  ngOnInit(): void {
  }

  addLike(member: Member) {
    this.memberService.addLike(member.username).subscribe(() => {
    this.toastr.success("You have liked " + member.knownAs);
    
    if(member!== null){
      this.toastr.error("You like this!");
    }
       
    })
  }

  onClick() {
    this.isActive = !this.isActive;
  }

}
