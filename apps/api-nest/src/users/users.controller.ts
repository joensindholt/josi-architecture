import {
  Body,
  Controller,
  Delete,
  Get,
  HttpCode,
  Logger,
  NotFoundException,
  Param,
  Post,
  Query,
  Response
} from '@nestjs/common';
import { Response as Res } from 'express';

import { UsersService } from './users.service';
import { CreateUserRequest } from './user';

@Controller('users')
export class UsersController {
  private readonly logger = new Logger(UsersController.name);

  constructor(private readonly usersService: UsersService) {}

  @Post()
  async create(@Body() createUserRequest: CreateUserRequest, @Response() res: Res) {
    this.logger.log('Creating user');
    const id = await this.usersService.create(createUserRequest);
    return res.set({ Location: `/users/${id}` }).json({ id });
  }

  @Get()
  async find(@Query('orderby') orderby: string) {
    this.logger.log('Finding all users');
    const users = await this.usersService.findAll(orderby);
    return {
      users
    };
  }

  @Get(':id')
  async findOne(@Param('id') id: string) {
    this.logger.log('Finding user');
    const user = await this.usersService.findOne(id);
    if (user === undefined) {
      throw new NotFoundException();
    }
    return user;
  }

  @Delete(':id')
  @HttpCode(204)
  async remove(@Param('id') id: string) {
    this.logger.log('Removing user');
    await this.usersService.remove(id);
  }
}
