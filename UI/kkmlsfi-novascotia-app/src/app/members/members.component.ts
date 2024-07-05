import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MemberService } from './services/member.service';
import { Member } from './models/member.model';
import { Observable, Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-members',
  standalone: true,
  templateUrl: './members.component.html',
  styleUrl: './members.component.css',
  imports: [RouterLink, CommonModule]
})
export class MembersComponent implements OnInit, OnDestroy {
  members$?: Observable<Member[]>;
  private deleteMemberSubscription?: Subscription;
  constructor(private memberService: MemberService) {

  }
  ngOnDestroy(): void {
    this.deleteMemberSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.loadMembers();
  }

  onDeleteMember(member: Member): void {
    if(confirm(`Are you sure to delete ${member.firstName} ${member.lastName}?`)) {
      if(member.memberId > 0) {
        this.deleteMemberSubscription = this.memberService.deleteMember(member.memberId)
        .subscribe({
          next: (response) => {
            this.loadMembers();
          }
        });
      }
    }
  }

  private loadMembers() {
    this.members$ = this.memberService.getAllMembers();
  }
}
