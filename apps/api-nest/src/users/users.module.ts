import { Module } from '@nestjs/common';
import { UsersController } from './users.controller';
import { UsersService } from './users.service';
import { DatabaseModule } from '../database/database.module';

@Module({
  controllers: [UsersController],
  providers: [UsersService],
  imports: [DatabaseModule],
})
export class UsersModule {}
