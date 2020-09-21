import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './SearchTab.css';

export class SearchTab extends Component {
    static displayName = SearchTab.name;

    constructor(props) {
        super(props);
        this.onClick = this.onClick.bind(this);
        this.onLogout = this.onLogout.bind(this);
        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    onClick() {
        let elem = document.getElementById("search");
        let url;
        if (elem.value != "")
            url = "https://localhost:5011/search-" + elem.value;
        else
            url = "https://localhost:5011/";
        window.location.replace(url);
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
    }

    componentDidMount() {
    }

    render() {
        return (
            <header>
                <Navbar id="second-nav" className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand><input type="text" placeholder="Search" id="search" autofocus /><p onClick=
                            {this.onClick} id="btn"><b>Go!</b></p></NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink id="AddProduct" tag={Link} className="text-dark" to="/add_product">Add Product</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink id="Admin" tag={Link} className="text-dark" to="/admin_tools">Admin tools</NavLink>
                                </NavItem>
                                <NavItem id="User">
                                    <NavLink tag={Link} className="text-dark" to="/cabinet">Cabinet</NavLink>
                                    <img class="logoutImg" onClick={this.onLogout} src="./images/logout.png" />
                                </NavItem>
                                <NavItem>
                                    <NavLink id="Auth" tag={Link} className="text-dark" to="/log_in">Log In</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/basket">Basket (<span id="basketCost">0.00</span>)</NavLink>
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
