import axios from 'axios';
import setup from './global-setup';

module.exports = async function () {
  console.log('Global setup');
  await setup();

  axios.defaults.baseURL = 'https://localhost:44311';

  // wait for api to be ready
  /* eslint-disable */
  console.log(`Checking api health at ${axios.defaults.baseURL}/health`);

  let healthy = false;

  async function handleApiNotReady(data) {
    console.log('Waiting for dotnet api to be ready');
    if (data) {
      console.log(data);
    }
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
        await handleApiNotReady(response?.data);
      }
    } catch (err) {
      await handleApiNotReady(err);
    }
  }
};
