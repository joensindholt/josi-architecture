import axios from 'axios';

describe('PUT /api/users', () => {
  it('should update a user', async () => {
    const createResponse = await axios.post(`/api/users`, {
      name: 'Random Name',
    });

    const userId = createResponse.data.id;

    await axios.patch(`/api/users/${userId}`, {
      name: 'Updated Name',
    });

    const getResponse = await axios.get(`/api/users/${userId}`);
    expect(getResponse.data.id).toBe(userId);
    expect(getResponse.data.name).toBe('Updated Name');
  });
});
