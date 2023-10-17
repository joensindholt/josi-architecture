import config from './jest.config';

/* eslint-disable */
export default {
  ...config,
  displayName: 'api-test',
  globalSetup: '<rootDir>/src/support/global-setup.api-dotnet.ts',
  setupFiles: ['<rootDir>/src/support/test-setup.api-dotnet.ts']
};
