import axios from 'axios';

describe('GET /users', () => {
  it('should get all users', async () => {
    const res = await axios.get(`/users`);

    expect(res).not.toBeNull();
    expect(res.status).toBe(200);
    expect(res.data).not.toBeUndefined();
    expect(res.data.users).not.toBeUndefined();
    expect(res.data.users.length).toBeGreaterThan(0);
    expect(res.data.users[0].id.length).toBeGreaterThanOrEqual(1);
  });

  it('should sort users correctly', async () => {
    await axios.post('/users', { name: 'Anton' });
    await axios.post('/users', { name: 'Ålholm' });
    await axios.post('/users', { name: 'AAbenholm' });

    const res = await axios.get('/users?orderby=name');
    const users: { name: string }[] = res.data.users;

    expect(users.findIndex((u) => u.name == 'Anton')).toBeLessThan(users.findIndex((u) => u.name == 'Ålholm'));
    expect(users.findIndex((u) => u.name == 'Anton')).toBeLessThan(users.findIndex((u) => u.name == 'AAbenholm'));
    expect(users.findIndex((u) => u.name == 'AAbenholm')).toBeLessThan(users.findIndex((u) => u.name == 'Ålholm'));
  });
});
