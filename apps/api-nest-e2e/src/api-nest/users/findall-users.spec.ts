import axios from 'axios';

describe('GET /api/users', () => {
  it('should get all users', async () => {
    const res = await axios.get(`/api/users`);

    expect(res.status).toBe(200);
    expect(res.data).not.toBeUndefined();
    expect(res.data.users).not.toBeUndefined();
    expect(res.data.users.length).toBeGreaterThan(0);
    expect(res.data.users[0].id.length).toBe(24);
  });

  it('should sort users correctly', async () => {
    await axios.post('/api/users', { name: 'Anton' });
    await axios.post('/api/users', { name: 'Ålholm' });
    await axios.post('/api/users', { name: 'AAbenholm' });

    const res = await axios.get('/api/users?orderby=name');
    const users: { name: string }[] = res.data.users;

    expect(users.findIndex((u) => u.name == 'Anton')).toBeLessThan(users.findIndex((u) => u.name == 'Ålholm'));
    expect(users.findIndex((u) => u.name == 'Anton')).toBeLessThan(users.findIndex((u) => u.name == 'AAbenholm'));
    expect(users.findIndex((u) => u.name == 'AAbenholm')).toBeLessThan(users.findIndex((u) => u.name == 'Ålholm'));
  });
});
