/* eslint-disable */
import axios from 'axios';
import setup from './test-setup.js';

module.exports = async function () {
  console.log(
    '*********************************\nRunning tests againt the dotNet api\n*********************************'
  );
  axios.defaults.baseURL = 'https://localhost:44311';
  setup();
};
