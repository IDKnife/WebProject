import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import './ClientPage.css';

export class ClientPage extends Component {
    static displayName = ClientPage.name;

    constructor(props) {
        super(props);
        this.updateUserAccess = this.updateUserAccess.bind(this);
        this.deleteUser = this.deleteUser.bind(this);
        this.state = {
            client: {},
            IsLoaded: false,
        };
    }

    async deleteUser() {
        await axios.post(`https://localhost:5001/api/Client/DeleteClient/${this.state.client.id}`,
            {},
            { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } });
        window.location.replace(`https://localhost:5011/clients`);
    }

    async updateUserAccess() {
        this.state.client.access = document.getElementById("accessChange").selectedIndex;
        await axios.post(`https://localhost:5001/api/Client/UpdateClient`,
            this.state.client,
            { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } });
    }

    async componentDidMount() {
        let url = `https://localhost:5001/api/Client/GetClient/` + `${this.props.match.params.id}`;
        await axios.get(url, { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } })
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, client: data });
                document.getElementById("accessChange").options.selectedIndex = this.state.client.access;
                if (this.state.client.access == 0) {
                    document.getElementById("accessChange").remove(2);
                    document.getElementById("accessChange").remove(1);
                }
            })
            .catch(console.log);
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
                <div class="innerPage">
                    <div><p>Email: </p><p>{client.email}</p></div>
                    <div><p>Firstname: </p><p>{client.firstName}</p></div>
                    <div><p>Secondname: </p><p>{client.secondName}</p></div>
                    <div><p>Lastname: </p><p>{client.lastName}</p></div>
                    <div><p>Phonenumber: </p><p>{client.phoneNumber}</p></div>
                    <div>
                        <select onChange={this.updateUserAccess} id="accessChange">
                            <option>Admin</option>
                            <option>Moderator</option>
                            <option>User</option>
                        </select>
                        <p class="btn" onClick={this.deleteUser}>Delete</p>
                    </div>
                </div>
            );
        }
    }
}
