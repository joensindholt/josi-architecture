import { Module } from '@nestjs/common';
import { ConfigModule } from '@nestjs/config';
import { databaseConnectionFactory, databaseConnection } from './database.connection.factory';

@Module({
  imports: [ConfigModule],
  providers: [
    {
      provide: databaseConnection,
      useFactory: databaseConnectionFactory
    }
  ],
  exports: [databaseConnection]
})
export class DatabaseModule {}
