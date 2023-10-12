// eslint-disable-next-line @typescript-eslint/no-unused-vars
import styles from './app.module.scss';
import Menu from './components/menu/menu';

import { Route, Routes, Link } from 'react-router-dom';
import Users from './users/pages/users/users';
import TopBar from './components/top-bar/top-bar';

export function App() {
  const users = Users({});

  return (
    <div className={styles['layout']}>
      <div className={styles['left-menu'] + ' border-end d-none d-md-block'}>
        <Menu />
      </div>
      <div className={styles['top-bar'] + ' border-bottom'}>
        <TopBar />
      </div>
      <div className={styles['main-content'] + ' px-5 py-4 bg-body-tertiary'}>
        <Routes>
          <Route path="/" Component={() => users} />
          <Route
            path="/page-2"
            element={
              <div>
                <Link to="/">Click here to go back to root page.</Link>
              </div>
            }
          />
        </Routes>
      </div>

      {/* END: routes */}
    </div>
  );
}

export default App;
