import axios from 'axios';
import https from 'https';

export default async function () {
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
}
