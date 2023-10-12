import { NavLink } from 'react-router-dom';

/* eslint-disable-next-line */
export interface MenuProps {}

export function Menu(props: MenuProps) {
  return (
    <div className="d-flex flex-column flex-shrink-0 p-3 h-100" style={{ width: '280px' }}>
      <a href="/" className="d-flex align-items-center ms-2 mb-md-4 me-md-auto link-body-emphasis text-decoration-none">
        <i className="bi-bootstrap me-2 fs-2"></i>
        <span className="fs-4">JosiArchitecture</span>
      </a>
      <div className="text-uppercase text-secondary mb-3">Main menu</div>
      <ul className="nav nav-pills flex-column mb-auto">
        <li className="nav-item">
          <NavLink to="/" className="nav-link" aria-current="page">
            <i className="bi-house me-2"></i>
            <span>Users</span>
          </NavLink>
        </li>
        <li>
          <NavLink to="/page-2" className="nav-link">
            <i className="bi-speedometer2 me-2"></i>
            <span>Other page</span>
          </NavLink>
        </li>
      </ul>
    </div>
  );
}

export default Menu;
