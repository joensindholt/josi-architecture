import axios from 'axios';

describe('POST /users', () => {
  it('should create a user and return the user id for that user', async () => {
    const response = await axios.post(`/users`, {
      name: 'Random Name'
    });

    expect(response).toBeTruthy();
    expect(response.status).toBe(201);
    expect(response.data.id).not.toBeUndefined();
    expect(typeof response.data.id).toBe('string');
  });

  it('should not allow users with no name', async () => {
    const response = await axios.post(`/users`, { wrongName: 'John' });

    expect(response).toBeTruthy();
    expect(response.status).toBe(400);
    const data = await response.data;
    expect(data.type).toBe('bad-request');
    expect(data.title).toBe("Your request parameters didn't validate");
    expect(data['invalid-params']).toEqual(
      expect.arrayContaining([{ name: 'name', reason: 'name should not be empty' }])
    );
  });
});
