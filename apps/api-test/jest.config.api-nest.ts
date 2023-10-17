import config from './jest.config';

/* eslint-disable */
export default {
  ...config,
  displayName: 'api-nest-api',
  globalSetup: '<rootDir>/src/support/global-setup.api-nest.ts',
  setupFiles: ['<rootDir>/src/support/test-setup.api-nest.ts']
};
