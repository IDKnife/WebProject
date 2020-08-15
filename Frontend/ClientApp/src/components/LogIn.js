import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import axios from "C:/Users/ArhiS/node_modules/axios";

export class LogIn extends Component {
    static displayName = LogIn.name;

    constructor(props) {
        super(props);
        this.onClickLogIn = this.onClickLogIn.bind(this);
        this.state = {
        };
    }

    onClickLogIn() {
        this.props.onClientLogin(document.getElementById("email").value, document.getElementById("password").value);
    }

    render() {
        return (
            <div class="formContainer">
                <form id="logIn">
                    <input type="email" id="email" placeholder="Email"></input>
                    <input type="password" id="password" placeholder="Password"></input>
                    <input type="button" id="logInButton" value="Log In" onClick={this.onClickLogIn}/>
                </form>
                <NavLink tag={Link} className="text-dark" to="/sign_in"><p>Sign In</p></NavLink>
            </div>
        );
    }
}
