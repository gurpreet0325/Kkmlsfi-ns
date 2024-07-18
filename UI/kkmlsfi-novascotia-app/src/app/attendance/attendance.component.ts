import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AttendanceService } from './services/attendance.service';
import { Attendance } from './models/attendance.model';
import { AttendancesRequest } from './models/attendance-request.model';
import { AuthService } from '../login/services/auth.service';
import { RouterLink } from '@angular/router';
import { formatDate } from 'date-fns';
import { UpdateAttendanceRequest } from './models/update-attendance-request.model';


@Component({
  selector: 'app-attendance',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './attendance.component.html',
  styleUrl: './attendance.component.css'
})
export class AttendanceComponent implements OnInit {
  attendances$?: Observable<Attendance[]>;

  constructor(private attendanceService: AttendanceService, private authService: AuthService) {

  }

  ngOnInit(): void {
    this.loadAllAttendances();
  }

  onAddAttendance() {
    this.attendances$?.subscribe({
      next: (list) => {
        const hasNotFinalAttendance = list.some((a) => {
          return a.isFinalized == false;
        });

        if (!hasNotFinalAttendance) {
          const dateToday = new Date();
          const newAttendance: AttendancesRequest = {
            attendanceDate: dateToday,
            userEmail: this.authService.getUser()?.email,
            actionDateTime: dateToday
          };
          this.attendanceService.addAttendance(newAttendance)
          .subscribe({
            next: (response) => {
              this.loadAllAttendances();
            }
          });
        } else {
          alert('Unable to proceed with the request. Please ensure the previous attendance is finalized before continuing.')
        }
      }
    })
  }

  onDeleteAttendance(attendance: Attendance) {
    if (confirm(`Are you sure you want to delete the attendance on ${formatDate(attendance.attendanceDate,'MMMM dd, yyyy')}?`)) {
      const dateToday = new Date();
      const updatedAttendance: UpdateAttendanceRequest = {
        attendanceId: attendance.attendanceId,
        userEmail: this.authService.getUser()?.email,
        actionDateTime: dateToday,
        isRemovedFromView: true,
        isFinal: false
      };
      this.attendanceService.updateAttendance(updatedAttendance)
      .subscribe({
        next: (response) => {
          this.loadAllAttendances();
        }
      })
    }
  }

  private loadAllAttendances() {
    this.attendances$ = this.attendanceService.getAllAttendances();
  }
}
