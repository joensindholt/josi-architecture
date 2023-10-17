import { BadRequestException, Logger, ValidationError, ValidationPipe } from '@nestjs/common';
import { NestFactory } from '@nestjs/core';
import { HttpExceptionFilter } from 'nest-problem-details-filter';
import * as fs from 'fs';
import * as path from 'path';

import { AppModule } from './app/app.module';

async function bootstrap() {
  const ssl = process.env.SSL === 'true' ? true : false;
  let httpsOptions = null;
  if (ssl) {
    const keyPath = process.env.SSL_KEY_PATH || '';
    const certPath = process.env.SSL_CERT_PATH || '';

    httpsOptions = {
      key: fs.readFileSync(path.join(__dirname, keyPath)),
      cert: fs.readFileSync(path.join(__dirname, certPath))
    };
  }

  const app = await NestFactory.create(AppModule, { httpsOptions, logger: ['debug', 'verbose'] });

  app.useGlobalFilters(new HttpExceptionFilter());

  app.useGlobalPipes(
    new ValidationPipe({
      exceptionFactory(errors: ValidationError[]) {
        return new BadRequestException({
          message: "Your request parameters didn't validate",
          error: {
            'invalid-params': errors.map(e => ({
              name: e.property,
              reason: Object.values(e.constraints)[0]
            }))
          }
        });
      }
    })
  );

  const port = process.env.PORT || 3000;
  const host = process.env.HOST || 'localhost';

  await app.listen(port, host, () => {
    Logger.log(`🚀 Application is running on: https://${host}:${port}`);
  });
}

bootstrap();
