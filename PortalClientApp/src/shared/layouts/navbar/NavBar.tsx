import { Link } from "react-router-dom";
import './NavBar.scss';
export function NavBar() {

    return (
      <nav>
        <Link to={'/'}>Library</Link>
        <Link to={'/'}>Collections</Link>
        <Link to={'/'}>Notes and Highlights</Link>
        <Link to={'/'}>Account</Link>
      </nav>
    );
}