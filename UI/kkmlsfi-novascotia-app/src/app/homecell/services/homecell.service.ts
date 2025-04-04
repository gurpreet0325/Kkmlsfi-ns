import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Homecell } from '../models/homecell.model';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class HomecellService {

  constructor(private http: HttpClient) { }

  getAllHomecells(): Observable<Homecell[]> {
    return this.http.get<Homecell[]>(`${environment.apiBaseUrl}/api/Homecell/GetAllHomecells`)
  }
}
