import axios from 'axios';

import setup from './test-setup';

module.exports = async function () {
  await setup();
  axios.defaults.baseURL = 'https://localhost:44310';
};
