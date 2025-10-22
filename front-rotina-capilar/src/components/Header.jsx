import { Link, useLocation} from "react-router-dom";
import "../styles/Header.css";

function Header() {
    const location = useLocation();

    return (
        <header className="header">
            <nav>
                <ul className="menu">
                    <li><Link to="/" className={location.pathname === "/" ? "active" : ""}>Home</Link></li>
                    <li><Link to="/produtos" className={location.pathname === "/produtos" ? "active" : ""}>Produtos</Link></li>
                    <li><Link to="/lavagens" className={location.pathname === "/lavagens" ? "active" : ""}>Lavagens</Link></li>
                    <li><Link to="/cuidados" className={location.pathname === "/cuidados" ? "active" : ""}>Cuidados</Link></li>
                </ul>
            </nav>
        </header>
    );
}

export default Header;