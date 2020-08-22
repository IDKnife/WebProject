import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import './Cabinet.css';

export class Cabinet extends Component {
    static displayName = Cabinet.name;

    constructor(props) {
        super(props);
        this.UpdatePersonalData = this.UpdatePersonalData.bind(this);
        this.Confirm = this.Confirm.bind(this);
        this.ChangePassword = this.ChangePassword.bind(this);
        this.state = {
            client: {},
            IsLoaded: false,
        };
    }

    async ChangePassword() {
        let old = document.getElementById("oldPassword").value;
        let newOne = document.getElementById("newPassword").value;
        let repeated = document.getElementById("repeatedNewPassword").value;
        if (old === this.state.client.password) {
            if (newOne === repeated) {
                if (newOne === old) {
                    alert("The new password cannot be the same as the old one");
                    return;
                }
                this.state.client.password = newOne;
                await axios.post('https://localhost:5001/api/Client/UpdateClient', this.state.client, { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } })
                    .then(() => this.setState({ client: this.state.client }));
                document.getElementById("oldPassword").value = "";
                document.getElementById("newPassword").value = "";
                document.getElementById("repeatedNewPassword").value = "";
            } else {
                alert("New password and repeated one are not the same");
            }
        } else {
            alert("Wrong password");
        }
    }

    async Confirm() {
        let client = this.state.client;
        document.getElementById("firstNameChange").style.display = "none";
        document.getElementById("secondNameChange").style.display = "none";
        document.getElementById("lastNameChange").style.display = "none";
        document.getElementById("phoneNumberChange").style.display = "none";
        document.getElementById("emailChange").style.display = "none";
        document.getElementById("confirm").style.display = "none";
        document.getElementById("firstName").style.display = "block";
        document.getElementById("secondName").style.display = "block";
        document.getElementById("lastName").style.display = "block";
        document.getElementById("phoneNumber").style.display = "block";
        document.getElementById("email").style.display = "block";
        document.getElementById("updatePersonalData").style.display = "block";
        client.firstName = document.getElementById("firstNameChange").value;
        client.secondName = document.getElementById("secondNameChange").value;
        client.lastName = document.getElementById("lastNameChange").value;
        client.phoneNumber = document.getElementById("phoneNumberChange").value;
        client.email = document.getElementById("emailChange").value;
        await axios.post('https://localhost:5001/api/Client/UpdateClient', client, { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } })
            .then(() => this.setState({ client: client }));
    }

    UpdatePersonalData() {
        let client = this.state.client;
        document.getElementById("firstNameChange").style.display = "block";
        document.getElementById("secondNameChange").style.display = "block";
        document.getElementById("lastNameChange").style.display = "block";
        document.getElementById("phoneNumberChange").style.display = "block";
        document.getElementById("emailChange").style.display = "block";
        document.getElementById("confirm").style.display = "block";
        document.getElementById("firstName").style.display = "none";
        document.getElementById("secondName").style.display = "none";
        document.getElementById("lastName").style.display = "none";
        document.getElementById("phoneNumber").style.display = "none";
        document.getElementById("email").style.display = "none";
        document.getElementById("updatePersonalData").style.display = "none";
        document.getElementById("firstNameChange").value = client.firstName;
        document.getElementById("secondNameChange").value = client.secondName;
        document.getElementById("lastNameChange").value = client.lastName;
        document.getElementById("phoneNumberChange").value = client.phoneNumber;
        document.getElementById("emailChange").value = client.email;
    }

    async componentDidMount() {
        await axios.get(`https://localhost:5001/api/Client/GetClient/${sessionStorage.getItem("client_id")}`,
            { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } })
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, client: data })
            })
            .catch(console.log);
        document.getElementById("firstNameChange").style.display = "none";
        document.getElementById("secondNameChange").style.display = "none";
        document.getElementById("lastNameChange").style.display = "none";
        document.getElementById("phoneNumberChange").style.display = "none";
        document.getElementById("emailChange").style.display = "none";
        document.getElementById("confirm").style.display = "none";
    }

    render() {
        var { IsLoaded, client } = this.state;
        if (!IsLoaded) {
            return (
                <p>
                    Loading.
                </p>
            );
        }
        else {
            return (
                <div id="cabinet_container">
                    <div>
                        <p id="firstName">{client.firstName}</p>
                        <input type="text" id="firstNameChange"></input>
                        <p id="secondName">{client.secondName}</p>
                        <input type="text" id="secondNameChange"></input>
                        <p id="lastName">{client.lastName}</p>
                        <input type="text" id="lastNameChange"></input>
                        <p id="phoneNumber">{client.phoneNumber}</p>
                        <input type="text" id="phoneNumberChange"></input>
                        <p id="email">{client.email}</p>
                        <input type="text" id="emailChange"></input>
                        <div class="btn" id="updatePersonalData" onClick={this.UpdatePersonalData}>Update personal data</div>
                        <div class="btn" id="confirm" onClick={this.Confirm}>Confirm</div>
                    </div>
                    <div>
                        <input type="password" id="oldPassword" placeholder="Old password"></input>
                        <input type="password" id="newPassword" placeholder="New password"></input>
                        <input type="password" id="repeatedNewPassword" placeholder="Repeat new password"></input>
                        <div class="btn" onClick={this.ChangePassword}>Change password</div>
                    </div>
                    <div>
                        <NavLink id="Orders" tag={Link} className="text-dark" to="/client_orders">Orders</NavLink>
                    </div>
                </div>
            );
        }
    }
}
