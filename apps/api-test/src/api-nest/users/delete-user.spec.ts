import axios from 'axios';

describe('DELETE /users', () => {
  it('should delete a user', async () => {
    // arrange
    const createResponse = await axios.post(`/users`, { name: 'Random Name' });

    const data = createResponse.data;
    const userId = data.id;

    const getResponse = await axios.get(`/users/${userId}`);
    expect(getResponse.data.id).toBe(userId);

    // act
    const deleteResponse = await axios.delete(`/users/${userId}`);

    // assert
    expect(deleteResponse.status).toBe(204);
    const getDeletedResponse = await fetch(`${axios.defaults.baseURL}/users/${userId}`);
    expect(getDeletedResponse.status).toBe(404);
  });
});
