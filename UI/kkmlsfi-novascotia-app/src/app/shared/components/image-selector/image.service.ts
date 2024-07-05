import { Injectable } from '@angular/core';
import { MemberRequest } from '../../../members/models/member-request.model';
import { Observable } from 'rxjs';
import { MemberDisplayPicture } from '../../models/member-display-picture.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http: HttpClient) { }

  getMemberDisplayPicture(memberId: number): Observable<MemberDisplayPicture> {
    return this.http.get<MemberDisplayPicture>(`${environment.apiBaseUrl}/api/Images/${memberId}`)
  }

  uploadMemberDisplayPicture(file: File, fileName: string, memberID: string): Observable<MemberDisplayPicture> {
    const formData = new FormData;
    formData.append('file', file);
    formData.append('fileName', fileName);
    formData.append('memberId', memberID);

    return this.http.post<MemberDisplayPicture>(`${environment.apiBaseUrl}/api/Images`,formData);
  }
}
