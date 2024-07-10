import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MemberRequest } from '../models/member-request.model';
import { Observable } from 'rxjs';
import { Member } from '../models/member.model';
import { environment } from '../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class MemberService {

  constructor(private http: HttpClient) { }

  getAllMembers(searchFilter?: string, sortBy?: string, sortDirection?: string, pageNumber?: number, pageSize?: number): Observable<Member[]> {
    let params = new HttpParams();

    if (searchFilter) {
      params = params.set('searchFilter', searchFilter)
    }

    if (sortBy) {
      params = params.set('sortBy', sortBy)
    }

    if (sortDirection) {
      params = params.set('sortDirection', sortDirection)
    }

    if (pageNumber) {
      params = params.set('pageNumber', pageNumber)
    }

    if (pageSize) {
      params = params.set('pageSize', pageSize)
    }

    return this.http.get<Member[]>(`${environment.apiBaseUrl}/api/Members/GetAllMembers`, {
      params: params
    });
  }

  getMemberById(id: number): Observable<Member> {
    return this.http.get<Member>(`${environment.apiBaseUrl}/api/Members/GetMemberById/${id}`);
  }

  addMember(model: MemberRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/Members/CreateMember?addAuth=true`, model);
  }

  updateMember(model: MemberRequest): Observable<void> {
    return this.http.put<void>(`${environment.apiBaseUrl}/api/Members/UpdateMember?addAuth=true`, model);
  }

  deleteMember(model: MemberRequest) {
    return this.http.put<Member>(`${environment.apiBaseUrl}/api/Members/DeleteMember/?addAuth=true`, model);
  }

  // deleteMember(id: number) {
  //   return this.http.delete<Member>(`${environment.apiBaseUrl}/api/Members/${id}?addAuth=true`);
  // }

  getMemberTotalCount(): Observable<number> {
    return this.http.get<number>(`${environment.apiBaseUrl}/api/Members/count`);
  }
}
