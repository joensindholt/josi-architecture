import { Logger } from '@nestjs/common';
import { Db, MongoClient } from 'mongodb';

export const databaseConnection = 'DATABASE_CONNECTION';

export async function databaseConnectionFactory(): Promise<Db> {
  const logger = new Logger('Database connection');
  logger.log('Waiting for database to be ready');
  const ready = false;
  while (!ready) {
    try {
      const databaseConnectionString = process.env.DATABASE_CONNECTIONSTRING;
      const client = await MongoClient.connect(databaseConnectionString);
      return client.db('josi');
    } catch (err) {
      logger.log('Waiting for database to be ready', err);
      await new Promise(resolve => {
        setTimeout(() => {
          resolve(0);
        }, 1000);
      });
    }
  }
}
