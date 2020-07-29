import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './SearchTab.css';

export class SearchTab extends Component {
    static displayName = SearchTab.name;

    constructor(props) {
        super(props);
        this.onClick = this.onClick.bind(this);
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
            url = "https://localhost:5011/"
        window.location.href = url;
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    componentDidMount() {

        console.log(this.props.order);
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
                                    <NavLink tag={Link} className="text-dark" to="#">Sign In</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/basket">Basket (<span id="basketCost">0.00</span>)</NavLink>
                                </NavItem>
                                {/*
                                    <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/add_product">Add Product</NavLink>
                                    </NavItem>
                                */}
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
