import { Inject, Injectable, OnModuleInit } from '@nestjs/common';
import { Db, Document, ObjectId, WithId } from 'mongodb';

import { CreateUserRequest, User } from './user';
import { AppConfigService } from '../configuration/app-config-service';
import { databaseConnection } from '../database/database.connection.factory';

@Injectable()
export class UsersService implements OnModuleInit {
  private readonly collectionName = 'users';

  constructor(@Inject(databaseConnection) private db: Db, private configService: AppConfigService) {}

  async onModuleInit(): Promise<void> {
    await this.initializeUsersCollection();
  }

  async findAll(orderby?: string): Promise<User[]> {
    let query = this.db.collection(this.collectionName).find();

    if (orderby) {
      query = query.sort({ [orderby]: 1 });
    }

    const docs = await query.toArray();
    const users: User[] = docs.map(this.mapToUser);
    return users;
  }

  async findOne(id: string): Promise<User | undefined> {
    const collection = this.db.collection(this.collectionName);
    const doc = await collection.findOne({
      _id: new ObjectId(id)
    });

    return doc ? this.mapToUser(doc) : undefined;
  }

  async create(request: CreateUserRequest): Promise<string> {
    const collection = this.db.collection(this.collectionName);
    const result = await collection.insertOne({
      name: request.name
    });
    return result.insertedId.toString();
  }

  async remove(id: string) {
    const collection = this.db.collection(this.collectionName);
    await collection.deleteOne({
      _id: new ObjectId(id)
    });
  }

  private mapToUser(document: WithId<Document>) {
    const user = document;

    user.id = document._id;
    delete user._id;

    return user as unknown as User;
  }

  private async initializeUsersCollection() {
    const collections = await this.db.listCollections(null, { nameOnly: true }).next();

    if (!collections) {
      this.db.createCollection(this.collectionName, {
        collation: {
          locale: 'da'
        }
      });
      if (this.configService.seedDatabase) {
        await this.seedUsers();
      }
    }
  }

  private async seedUsers() {
    const collection = this.db.collection(this.collectionName);
    await collection.insertMany([
      {
        name: 'John Doe'
      },
      {
        name: 'Jane Doe'
      }
    ]);
  }
}
