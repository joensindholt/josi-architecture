import axios from 'axios';
import setup from './global-setup';

module.exports = async function () {
  await setup();

  axios.defaults.baseURL = 'https://localhost:44311';

  // wait for api to be ready
  /* eslint-disable */
  console.log(`Checking api health at ${axios.defaults.baseURL}/health`);

  let healthy = false;

  async function handleApiNotReady() {
    console.log('Waiting for api to be ready');
    await new Promise(resolve =>
      setTimeout(() => {
        resolve(0);
      }, 1000)
    );
  }

  while (!healthy) {
    try {
      const response = await axios.get('/health');
      if (response?.status === 200) {
        healthy = true;
      } else {
        await handleApiNotReady();
      }
    } catch (err) {
      await handleApiNotReady();
    }
  }
};
