import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MemberRequest } from '../models/member-request.model';
import { Observable } from 'rxjs';
import { Member } from '../models/member.model';
import { environment } from '../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class MemberService {

  constructor(private http: HttpClient) { }

  getAllMembers(): Observable<Member[]> {
    return this.http.get<Member[]>(`${environment.apiBaseUrl}/api/Members`);
  }

  getMemberById(id: number): Observable<Member> {
    return this.http.get<Member>(`${environment.apiBaseUrl}/api/Members/${id}`);
  }

  addMember(model: MemberRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/Members`, model);
  }

  updateMember(model: MemberRequest): Observable<void> {
    return this.http.put<void>(`${environment.apiBaseUrl}/api/Members/`, model);
  }

  deleteMember(id: number) {
    return this.http.delete<Member>(`${environment.apiBaseUrl}/api/Members/${id}`);
  }
}
