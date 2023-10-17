import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { userRoutes } from './userRoutes';
import { UsersComponent } from './pages/users/users.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [UsersComponent],
  imports: [CommonModule, RouterModule.forChild(userRoutes), HttpClientModule],
  exports: [UsersComponent]
})
export class UsersModule {}
