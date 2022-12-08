import { Link, Outlet } from 'react-router-dom';
import './Header.scss';

export function Header() {
  return (
    <header>
      <div className="main-links">
        <Link to={`/`}>Portal - An Epub Reader</Link>
        <input type="text" placeholder='Search You Catalog' />
      </div>
    </header>
  );
}
