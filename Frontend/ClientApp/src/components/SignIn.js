import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import axios from "C:/Users/ArhiS/node_modules/axios";

export class SignIn extends Component {
    static displayName = SignIn.name;

    constructor(props) {
        super(props);
        this.onClickSignIn = this.onClickSignIn.bind(this);
        this.state = {
        };
    }

    onClickSignIn() {
        let client = {
            firstName: document.getElementById("firstName").value,
            secondName: document.getElementById("secondName").value,
            lastName: document.getElementById("lastName").value,
            phoneNumber: document.getElementById("phoneNumber").value,
            email: document.getElementById("email").value,
            password: document.getElementById("password").value,
            access: 2
        };
        axios.post('https://localhost:5021/api/Token/Register', client);
        window.location.replace('https://localhost:5011/log_in');
    }

    render() {
        return (
            <div class="formContainer">
                <form id="signIn">
                    <input type="text" id="firstName" placeholder="First Name"></input>
                    <input type="text" id="secondName" placeholder="Second Name"></input>
                    <input type="text" id="lastName" placeholder="Last Name"></input>
                    <input type="text" id="phoneNumber" placeholder="Phone"></input>
                    <input type="email" id="email" placeholder="Email"></input>
                    <input type="password" id="password" placeholder="Password"></input>
                    <input type="button" id="signInButton" value="Sign In" onClick={this.onClickSignIn}/>
                </form>
            </div >
        );
    }
}