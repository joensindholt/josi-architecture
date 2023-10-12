import { useEffect, useState } from 'react';

import { User } from '../../models/user';

/* eslint-disable-next-line */
export interface UsersProps {}

export function Users(props: UsersProps) {
  const [users, setUsers] = useState([]);

  const fetchUsers = () => {
    return fetch('https://localhost:5000/users', {
      mode: 'cors',
    })
      .then((res) => res.json())
      .then((d) => setUsers(d.users));
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  return (
    <div>
      <h2 className="pb-4 fw-semibold">
        <i className="bi-people me-2"></i>
        <span>Users</span>
      </h2>

      <div className="table-responsive border rounded">
        <table className="text-nowrap table align-middle m-0">
          <thead>
            <tr>
              <th>Name</th>
              <th>Role</th>
              <th>Last Activity</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {users.map((user: User, _index: number) => {
              return (
                <tr key={user.id}>
                  <td>
                    <div className="d-flex flex-column align-items-start">
                      <h5 className="mb-1">{user.name}</h5>
                      <p className="mb-0 text-secondary">{user.name.toLowerCase()}@example.com</p>
                    </div>
                  </td>
                  <td>Front End Developer</td>
                  <td>3 May, 2023</td>
                  <td>
                    <div className="dropdown">
                      <a className="text-muted text-primary-hover" href="/">
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          width="15px"
                          height="15px"
                          viewBox="0 0 24 24"
                          fill="none"
                          stroke="currentColor"
                          strokeWidth="2"
                          strokeLinecap="round"
                          strokeLinejoin="round"
                          className="text-muted"
                        >
                          <circle cx="12" cy="12" r="1"></circle>
                          <circle cx="12" cy="5" r="1"></circle>
                          <circle cx="12" cy="19" r="1"></circle>
                        </svg>
                      </a>
                    </div>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default Users;
