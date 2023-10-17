import axios from 'axios';
import https from 'https';

export default async function () {
  console.log('\nSetting up...\n');

  // Ignore SSL cert errors
  axios.defaults.httpAgent = new https.Agent({
    rejectUnauthorized: false
  });

  axios.interceptors.response.use(
    function (response) {
      return response;
    },
    function (error) {
      return Promise.resolve(error.response);
    }
  );

  globalThis.__TEARDOWN_MESSAGE__ = '\nTearing down...\n';
}
