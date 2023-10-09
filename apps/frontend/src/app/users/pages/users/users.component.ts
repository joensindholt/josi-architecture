import { Component } from '@angular/core';
import { Observable, of } from 'rxjs';
import { User } from '../../models/user';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent {
  users$: Observable<User[]>;

  constructor() {
    this.users$ = of([
      {
        name: 'John Doe',
      },
      {
        name: 'Jane Doe',
      },
      {
        name: 'Lis Doe',
      },
      {
        name: 'Peter Doe',
      },
      {
        name: 'McMillan Doe',
      },
    ]);
  }
}
