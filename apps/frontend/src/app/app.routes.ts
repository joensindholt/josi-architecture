import { Route } from '@angular/router';
import { MenuComponent } from './components/menu/menu.component';

export const appRoutes: Route[] = [
  {
    path: '',
    loadChildren: () => import('./users/users.module').then(m => m.UsersModule)
  },
  {
    path: 'other',
    component: MenuComponent
  }
];
