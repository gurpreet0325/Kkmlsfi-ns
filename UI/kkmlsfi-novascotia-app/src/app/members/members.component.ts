import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MemberService } from './services/member.service';
import { Member } from './models/member.model';
import { Observable, Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { AuthService } from '../login/services/auth.service';
import { FormsModule } from '@angular/forms';
import { defaultProperties, environment } from '../../environments/environment';

@Component({
  selector: 'app-members',
  standalone: true,
  templateUrl: './members.component.html',
  styleUrl: './members.component.css',
  imports: [RouterLink, CommonModule, FormsModule]
})
export class MembersComponent implements OnInit, OnDestroy {
  isAdmin: boolean = false;
  members$?: Observable<Member[]>;
  searchFilter: string = '';
  totalCount?: number;
  pageNumber = 1;
  pageSize = defaultProperties.pageSize;
  pageList: number[] = [];
  private deleteMemberSubscription?: Subscription;

  constructor(private memberService: MemberService, private authService: AuthService) {

  }
  ngOnDestroy(): void {
    this.deleteMemberSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.isAdmin = this.authService.isAdmin();

    this.memberService.getMemberTotalCount()
    .subscribe({
      next: (value) => {
        this.totalCount = value;

        this.pageList = new Array(Math.ceil(value / this.pageSize));

        this.loadMembers(undefined,undefined,undefined,this.pageNumber,this.pageSize);
      }
    });
  }

  onDeleteMember(member: Member): void {
    if(confirm(`Are you sure to delete ${member.firstName} ${member.lastName}?`)) {
      if(member.memberId > 0) {
        this.deleteMemberSubscription = this.memberService.deleteMember(member.memberId)
        .subscribe({
          next: (response) => {
            this.loadMembers(undefined,undefined,undefined,this.pageNumber,this.pageSize);
          }
        });
      }
    }
  }

  onSearch(searchFilter: string) {
    this.loadMembers(searchFilter,undefined,undefined,this.pageNumber,this.pageSize);
  }

  sort(sortBy: string, sortDirection: string) {
    this.loadMembers(this.searchFilter, sortBy, sortDirection, this.pageNumber, this.pageSize);
  }

  onPageChange(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.loadMembers(this.searchFilter,undefined,undefined,this.pageNumber,this.pageSize);
  }

  onPreviousPage() {
    if (this.pageNumber > 1) {
      this.pageNumber -= 1;
      this.loadMembers(this.searchFilter,undefined,undefined,this.pageNumber,this.pageSize);
    }
  }

  onNextPage() {
    if (this.pageNumber < this.pageList.length) {
      this.pageNumber += 1;
      this.loadMembers(this.searchFilter,undefined,undefined,this.pageNumber,this.pageSize);
    }
  }

  private loadMembers(searchFilter?: string, sortBy?: string, sortDirection?: string, pageNumber?: number, pageSize?: number) {
    this.members$ = this.memberService.getAllMembers(searchFilter, sortBy, sortDirection, pageNumber, pageSize);
  }
}
