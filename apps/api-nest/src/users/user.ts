import { IsNotEmpty } from 'class-validator';

export interface User {
  id: string;
  name: string;
}

export class CreateUserRequest {
  @IsNotEmpty()
  name: string;
}

export class UpdateUserRequest {
  name: string;
}
