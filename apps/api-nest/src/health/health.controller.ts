import { Controller, Get, Inject } from '@nestjs/common';
import { HealthCheckService, HealthCheck, HealthIndicatorResult } from '@nestjs/terminus';
import { Db } from 'mongodb';
import { databaseConnection } from '../database/database.connection.factory';

@Controller('health')
export class HealthController {
  constructor(private health: HealthCheckService, @Inject(databaseConnection) private db: Db) {}

  @Get()
  @HealthCheck()
  check() {
    return this.health.check([
      () =>
        this.db
          .stats()
          .then(s => this.buildMongoHealthIndicatorResult(s.ok))
          .catch(() => this.buildMongoHealthIndicatorResult(false))
    ]);
  }

  private buildMongoHealthIndicatorResult(ok: boolean): HealthIndicatorResult | PromiseLike<HealthIndicatorResult> {
    return <HealthIndicatorResult>{
      db: {
        status: ok ? 'up' : 'down'
      }
    };
  }
}
