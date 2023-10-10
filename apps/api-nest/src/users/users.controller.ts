import { Body, Controller, Delete, Get, NotFoundException, Param, Patch, Post, Query } from '@nestjs/common';

import { UsersService } from './users.service';
import { CreateUserRequest, UpdateUserRequest } from './user';

@Controller('users')
export class UsersController {
  constructor(private readonly usersService: UsersService) {}

  @Post()
  async create(@Body() createUserRequest: CreateUserRequest) {
    const id = await this.usersService.create(createUserRequest);
    return {
      id,
    };
  }

  @Get()
  async find(@Query('orderby') orderby: string) {
    const users = await this.usersService.findAll(orderby);
    return {
      users,
    };
  }

  @Get(':id')
  async findOne(@Param('id') id: string) {
    const user = await this.usersService.findOne(id);
    if (user === undefined) {
      throw new NotFoundException();
    }
    return user;
  }

  @Patch(':id')
  async update(@Param('id') id: string, @Body() updateUserRequest: UpdateUserRequest) {
    return await this.usersService.update(id, updateUserRequest);
  }

  @Delete(':id')
  async remove(@Param('id') id: string) {
    return await this.usersService.remove(id);
  }
}
