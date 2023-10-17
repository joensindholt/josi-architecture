import { Module } from '@nestjs/common';
import { ConfigModule } from '@nestjs/config';

import { AppController } from './app.controller';
import { AppService } from './app.service';
import { UsersModule } from '../users/users.module';
import { ConfigurationModule } from '../configuration/configuration.module';
import { HealthModule } from '../health/health.module';

@Module({
  imports: [
    UsersModule,
    HealthModule,
    ConfigurationModule,
    ConfigModule.forRoot({
      isGlobal: true
    })
  ],
  controllers: [AppController],
  providers: [AppService]
})
export class AppModule {}
