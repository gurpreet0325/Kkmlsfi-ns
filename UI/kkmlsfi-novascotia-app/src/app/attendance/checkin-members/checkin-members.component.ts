import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription, tap } from 'rxjs';
import { MemberAttendance } from '../models/member-attendance.model';
import { AttendanceService } from '../services/attendance.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Attendance } from '../models/attendance.model';
import { MemberService } from '../../members/services/member.service';
import { Member } from '../../members/models/member.model';
import { MemberAttendanceRequest } from '../models/member-attendance-request.model';
import { AuthService } from '../../login/services/auth.service';
import { formatDate } from 'date-fns';
import { UpdateAttendanceRequest } from '../models/update-attendance-request.model';
import { UpdateMembersAttendanceRequest, ValueTypes } from '../models/update-member-attendance.model';

@Component({
  selector: 'app-checkin-members',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './checkin-members.component.html',
  styleUrl: './checkin-members.component.css'
})
export class CheckinMembersComponent implements OnInit, OnDestroy {
  membersAttendance$?: Observable<MemberAttendance[]>;
  members$?: Observable<Member[]>;
  paramSubscription?: Subscription;
  attendance?: Attendance;
  isAdmin: boolean = false;
  valueType = ValueTypes;
  userEmail = this.authService.getUser()?.email;

  constructor(private attendanceService: AttendanceService, 
    private memberService: MemberService,
    private authService: AuthService, 
    private router: Router, 
    private route: ActivatedRoute) {

  }

  ngOnDestroy(): void {
    this.paramSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.isAdmin = this.authService.isAdmin();
    this.paramSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.attendanceService.getAttendanceById(Number(params.get('id')))
        .subscribe({
          next: (response) => {
            this.attendance = response;
            this.loadmembersAttendance(this.attendance.attendanceId);
            this.members$ = this.memberService.getAllMembers(undefined,undefined,undefined,undefined,undefined);
          }
        });
      }
    })
  }

  onSelectMember(e: any) {
    if (e.target.value > 0) {
      const memberId: number = e.target.value;

      this.membersAttendance$?.subscribe({
        next: (list) => {
          const hasExistingMember = list.some((m) => { 
            return m.memberId == memberId;
          });

          if (!hasExistingMember) {
            this.addNewMember(memberId);
          }
        }
      });
    }
  }

  onDeleteMember(member: MemberAttendance) {
    this.attendanceService.deleteMembersAttendanceById(member.membersAttendanceId)
    .subscribe({
      next: (response) => {
        this.loadmembersAttendance(member.attendanceId);
      }
    })
  }
  
  onCancel() {
    this.router.navigateByUrl('/attendance');
  }

  onFormSubmit() {
    if (this.attendance && confirm('Finalizing this attendance will prevent users from making further modifications. Are you sure you want to continue?')) {
      const dateToday = new Date();
      const updatedAttendance: UpdateAttendanceRequest = {
        attendanceId: this.attendance.attendanceId,
        userEmail: this.userEmail,
        actionDateTime: dateToday,
        isRemovedFromView: false,
        isFinal: true
      };
      this.attendanceService.updateAttendance(updatedAttendance)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/attendance');
        }
      })
    }
  }

  private addNewMember(memberId: number) {
    if (this.attendance) {
      const dateToday = new Date();
      const memberAttendance: MemberAttendanceRequest = {
        attendanceId: this.attendance?.attendanceId,
        memberId: memberId,
        userEmail: this.userEmail,
        actionDateTime: dateToday
      };
      this.attendanceService.addMembersAttendance(memberAttendance)
      .subscribe({
        next: (response) => {
          this.loadmembersAttendance(memberAttendance.attendanceId);
        }
      });
    }
  }

  onAddTithesAndOfferings(membersAttendanceId: number, valueType: number, value: any) {
    console.log('start');
    const membersAttendance: UpdateMembersAttendanceRequest = {
      membersAttendanceId: membersAttendanceId,
      valueType: valueType,
      value: value,
      note: value,
      userEmail: this.userEmail,
      actionDateTime: new Date()
    };

    console.log(membersAttendance);

    this.attendanceService.updateMembersAttendance(membersAttendance)
      .subscribe({
        next: (response) => {
          
        }
      });
  }

  private loadmembersAttendance(attendanceId: number)
  {
    this.membersAttendance$ = this.attendanceService.GetMembersAttendanceById(attendanceId);
  }
}
