import axios from 'axios';
import setup from './global-setup';

module.exports = async function () {
  await setup();

  axios.defaults.baseURL = 'https://localhost:44310';

  // wait for api to be ready
  /* eslint-disable */
  console.log(`Checking api health at ${axios.defaults.baseURL}/health`);

  let healthy = false;
  while (!healthy) {
    try {
      const response = await axios.get('/health');
      if (!response) {
        throw new Error('Undefined response');
      }
      healthy = true;
    } catch (err) {
      console.log('Waiting for api to be ready');
      await new Promise(resolve =>
        setTimeout(() => {
          resolve(0);
        }, 1000)
      );
    }
  }
};
