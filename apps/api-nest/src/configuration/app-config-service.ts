import { Injectable } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';

@Injectable()
export class AppConfigService {
  constructor(private configService: ConfigService) {}

  get seedDatabase(): boolean {
    return this.configService.get('SEED_DATABASE') === 'true';
  }
}
