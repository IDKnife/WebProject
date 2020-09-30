import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.onLogout = this.onLogout.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    onLogout() {
        sessionStorage.setItem("client_id", "");
        sessionStorage.setItem("access_token", "");
        sessionStorage.setItem("access_level", "");
        sessionStorage.setItem("IsAuthorized", "");
        document.getElementById("User").style.display = "none";
        document.getElementById("Auth").style.display = "block";
        document.getElementById("Admin").style.display = "none";
        document.getElementById("AddProduct").style.display = "none";
        window.location.replace('https://localhost:5011');
    }

    render() {
        return (
            <header id="first">
                <Navbar id="first-nav" className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand>8(495)*******</NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/info">Info</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/delivery">Delivery</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/store_addresses">Store addresses</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/feedback">Feedback</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink id="Auth" tag={Link} className="text-dark" to="/log_in">Log In</NavLink>
                                </NavItem>
                                <NavItem id="User">
                                    <NavLink tag={Link} className="text-dark" to="/cabinet">Cabinet</NavLink>
                                    <img class="logoutImg" onClick={this.onLogout} src="./images/logout.png" />
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
