import { Component } from '@angular/core';
import { Observable, map } from 'rxjs';
import { GetUsersResponse, User } from '../../models/user';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent {
  users$: Observable<User[]>;

  constructor(private httpClient: HttpClient) {
    this.users$ = this.httpClient.get<GetUsersResponse>('https://localhost:5000/users').pipe(map(r => r.users));
  }
}
