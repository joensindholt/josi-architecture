/* eslint-disable */
import https from 'https';
import axios from 'axios';

export default async function () {
  axios.defaults.httpAgent = new https.Agent({
    rejectUnauthorized: false
  });

  // Disable axios throwing exceptions - we just want the raw response when testing
  axios.interceptors.response.use(
    function (response) {
      return response;
    },
    function (error) {
      return Promise.resolve(error.response);
    }
  );
}
