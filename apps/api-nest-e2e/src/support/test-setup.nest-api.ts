import axios from 'axios';
import setup from './test-setup.js';

module.exports = async function () {
  console.log(
    '*********************************\nRunning tests againt the Nest api\n*********************************'
  );
  axios.defaults.baseURL = 'https://localhost:44310';
  setup();
};
