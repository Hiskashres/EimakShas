import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../Models/User.model'

@Injectable({
  providedIn: 'root'
})

export class UserService {
  private apiUrl = 'https://localhost:7066/api/User'

  constructor(private http: HttpClient) {}

  addUser(user: User): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, user);
  }
}
