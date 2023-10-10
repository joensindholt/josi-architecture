import axios from 'axios';

describe('DELETE /api/users', () => {
  it('should delete a user', async () => {
    const createResponse = await axios.post(`/api/users`, {
      name: 'Random Name',
    });

    const userId = createResponse.data.id;

    const getResponse = await axios.get(`/api/users/${userId}`);
    expect(getResponse.data.id).toBe(userId);

    const deleteResponse = await axios.delete(`/api/users/${userId}`);
    expect(deleteResponse.status).toBe(200);

    const getDeletedResponse = await fetch(`${axios.defaults.baseURL}/api/users/${userId}`);
    expect(getDeletedResponse.status).toBe(404);
  });
});
