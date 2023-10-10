import axios from 'axios';

describe('POST /api/users', () => {
  it('should create a user and return the mongo object id for that user', async () => {
    const res = await axios.post(`/api/users`, {
      name: 'Random Name',
    });

    expect(res.status).toBe(201);
    expect(res.data.id).not.toBeUndefined();
    expect(res.data.id.length).toBe(24);
  });

  it('should not allow users with no name', async () => {
    const response = await fetch(`${axios.defaults.baseURL}/api/users`, {
      method: 'POST',
      body: JSON.stringify({ fname: 'John' }),
    });

    expect(response.status).toBe(400);

    const data = await response.json();
    expect(data.message).toContain('name should not be empty');
  });
});
