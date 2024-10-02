import { FC } from "react";
import "./_Header.scss";
import { NavLink } from "react-router-dom";
import logo from "../../../assets/PokeAppLogo.png";

const Header:FC = () => {
    return (
        <header className="header">
            <img className="header-logo" src={logo} alt="logo-pokeapp" />
            <nav className="header-nav">
                <ul>
                    <li>
                        <NavLink
                            className={({ isActive }) => (
                                isActive ? 'header-nav-link active' 
                                : 'header-nav-link inactive'
                            )}
                            title="Home"
                            end to="/home"
                        >
                            Home
                        </NavLink>
                    </li>
                    <li>
                        <NavLink
                            className={({ isActive }) => (
                                isActive ? `header-nav-link active` 
                                : `header-nav-link inactive`
                            )}
                            title="Team"
                            end to="/team"
                        >
                            Team
                        </NavLink>
                    </li>
                    <li>
                        <NavLink
                            className={({ isActive }) => (
                                isActive ? 'header-nav-link active' 
                                : 'header-nav-link inactive'
                            )}
                            title="Favorites"
                            end to="/favorites"
                        >
                            Favorites
                        </NavLink>
                    </li>
                    <li>
                        <button>Connexion</button>
                    </li>
                </ul>
            </nav>
        </header>
    )
}

export default Header;