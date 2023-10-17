import axios, { AxiosResponse } from 'axios';

describe('GET /users', () => {
  it('should get all users', async () => {
    // arrange
    await axios.post(`/users`, { name: 'Random Name' });

    // act
    let res: AxiosResponse;
    do {
      res = await axios.get(`/users`);
    } while (res?.data?.users?.length === 0);

    // assert
    expect(res).not.toBeNull();
    expect(res.status).toBe(200);
    expect(res.data).not.toBeUndefined();
    expect(res.data.users).not.toBeUndefined();
    expect(res.data.users.length).toBeGreaterThan(0);
    expect(res.data.users[0].id.length).toBeGreaterThanOrEqual(1);
  });

  it('should sort users correctly', async () => {
    // arrange
    await axios.post('/users', { name: 'Anton' });
    await axios.post('/users', { name: 'Ålholm' });
    await axios.post('/users', { name: 'AAbenholm' });

    // act
    let res: AxiosResponse;
    do {
      res = await axios.get('/users?orderby=name');
    } while (!res?.data?.users?.some(u => u.name === 'Anton'));

    // assert
    const users: { name: string }[] = res.data.users;
    expect(users.findIndex(u => u.name === 'Anton')).toBeLessThan(users.findIndex(u => u.name === 'Ålholm'));
    expect(users.findIndex(u => u.name === 'Anton')).toBeLessThan(users.findIndex(u => u.name === 'AAbenholm'));
    expect(users.findIndex(u => u.name === 'AAbenholm')).toBeLessThan(users.findIndex(u => u.name === 'Ålholm'));
  });
});
