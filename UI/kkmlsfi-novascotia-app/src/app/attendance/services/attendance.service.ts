import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MemberAttendance } from '../models/member-attendance.model';
import { environment } from '../../../environments/environment.development';
import { AttendancesRequest } from '../models/attendance-request.model';
import { Attendance } from '../models/attendance.model';
import { MemberAttendanceRequest } from '../models/member-attendance-request.model';
import { UpdateAttendanceRequest } from '../models/update-attendance-request.model';

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {

  constructor(private http: HttpClient) { }

  GetMembersAttendanceById(attendanceId: number): Observable<MemberAttendance[]> {
    return this.http.get<MemberAttendance[]>(`${environment.apiBaseUrl}/api/Attendance/GetMembersAttendanceById/${attendanceId}`)
  }

  getAttendanceById(attendanceId: number): Observable<Attendance> {
    return this.http.get<Attendance>(`${environment.apiBaseUrl}/api/Attendance/GetAttendanceById/${attendanceId}`)
  }

  getAllAttendances(): Observable<Attendance[]> {
    return this.http.get<Attendance[]>(`${environment.apiBaseUrl}/api/Attendance/GetAllAttendaces`)
  }

  addAttendance(model: AttendancesRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/Attendance/CreateAttendance`, model);
  }

  addMembersAttendance(model: MemberAttendanceRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/Attendance/CreateMembersAttendance`, model);
  }

  deleteMembersAttendanceById(memberAttendanceId: number) {
    return this.http.delete<MemberAttendance>(`${environment.apiBaseUrl}/api/Attendance/DeleteMembersAttendanceById/${memberAttendanceId}`)
  }

  updateAttendance(model: UpdateAttendanceRequest): Observable<void> {
    return this.http.put<void>(`${environment.apiBaseUrl}/api/Attendance/UpdateAttendance`, model);
  }
}
