import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { userRoutes } from './userRoutes';
import { UsersComponent } from './pages/users/users.component';

@NgModule({
  declarations: [UsersComponent],
  imports: [CommonModule, RouterModule.forChild(userRoutes)],
  exports: [UsersComponent],
})
export class UsersModule {}
