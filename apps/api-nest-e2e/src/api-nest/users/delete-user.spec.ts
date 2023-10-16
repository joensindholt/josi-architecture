import axios from 'axios';

describe('DELETE /users', () => {
  it('should delete a user', async () => {
    const createResponse = await axios.post(`/users`, {
      name: 'Random Name'
    });

    const data = createResponse.data;
    const userId = data.id;

    const getResponse = await axios.get(`/users/${userId}`);
    expect(getResponse.data.id).toBe(userId);

    const deleteResponse = await axios.delete(`/users/${userId}`);
    expect(deleteResponse.status).toBe(204);

    const getDeletedResponse = await fetch(`${axios.defaults.baseURL}/users/${userId}`);
    expect(getDeletedResponse.status).toBe(404);
  });
});
