import { Route } from '@angular/router';
import { UsersComponent } from './pages/users/users.component';

export const userRoutes: Route[] = [
  {
    path: '',
    component: UsersComponent,
  },
];
