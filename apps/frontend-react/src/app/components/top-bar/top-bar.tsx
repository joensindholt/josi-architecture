/* eslint-disable-next-line */
export interface TopBarProps {}

export function TopBar(props: TopBarProps) {
  return (
    <header className="p-3">
      <div className="d-flex align-items-center justify-content-between">
        <form className="col-auto flex-grow-1 me-3" role="search">
          <input type="search" className="form-control" placeholder="Search..." aria-label="Search" />
        </form>
        <div className="dropdown">
          <a
            href="/"
            className="d-flex align-items-center link-body-emphasis text-decoration-none dropdown-toggle"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            <img src="https://github.com/mdo.png" alt="" width="32" height="32" className="rounded-circle me-2" />
            <strong>mdo</strong>
          </a>
        </div>
      </div>
    </header>
  );
}

export default TopBar;
