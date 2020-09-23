import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import './Clients.css';


export class Clients extends Component {
    static displayName = Clients.name;

    constructor(props) {
        super(props);
        this.AccessConverter = this.AccessConverter.bind(this);
        this.sortAscendingEmails = this.sortAscendingEmails.bind(this);
        this.sortDescendingEmails = this.sortDescendingEmails.bind(this);
        this.sortAscendingAccess = this.sortAscendingAccess.bind(this);
        this.sortDescendingAccess = this.sortDescendingAccess.bind(this);
        this.state = {
            clients: {},
            IsLoaded: false
        };
    }
    AccessConverter(client) {
        switch (client.access) {
            case 0:
                return ("Admin");
            case 1:
                return ("Moderator");
            case 2:
                return ("User");
        }
    }
    sortAscendingEmails() {
        this.setState({
            clients: this.state.clients.sort(function (a, b) {
                if (a.email > b.email) return 1;
                if (a.email == b.email) return 0;
                if (a.email < b.email) return -1;
            })
        })
    }
    sortDescendingEmails() {
        this.setState({
            clients: this.state.clients.sort(function (a, b) {
                if (a.email > b.email) return -1;
                if (a.email == b.email) return 0;
                if (a.email < b.email) return 1;
            })
        })
    }
    sortAscendingAccess() {
        this.setState({
            clients: this.state.clients.sort(function (a, b) {
                if (a.access > b.access) return 1;
                if (a.access == b.access) return 0;
                if (a.access < b.access) return -1;
            })
        })
    }
    sortDescendingAccess() {
        this.setState({
            clients: this.state.clients.sort(function (a, b) {
                if (a.access > b.access) return -1;
                if (a.access == b.access) return 0;
                if (a.access < b.access) return 1;
            })
        })
    }
    async componentDidMount() {
        await axios.get("https://localhost:5001/api/Client/Clients",
                { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } })
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, clients: data })
            })
            .catch(console.log);
    }

    render() {
        var { IsLoaded, clients } = this.state;
        if (!IsLoaded) {
            return (
                <p>
                    Loading.
                </p>
            );
        }
        else {
            return (
                <div>
                    <ul>
                        <li class="li-flex">
                            <p>
                                <b>Email  </b>
                                <img onClick={this.sortAscendingEmails} class="sort" src="./images/up.png" />
                                <img onClick={this.sortDescendingEmails} class="sort" src="./images/down.png" />
                            </p>
                            <p>
                                <b>Access  </b>
                                <img onClick={this.sortAscendingAccess} class="sort" src="./images/up.png" />
                                <img onClick={this.sortDescendingAccess} class="sort" src="./images/down.png" />
                            </p>
                        </li>
                        {clients.map((client) => (
                            <li onClick={function () { window.location.replace(`https://localhost:5011/client_page${client.id}`)}} class="li-flex">
                                <p>{client.email}</p>
                                <p>{this.AccessConverter(client)}</p>
                            </li>
                        ))}
                    </ul>
                </div >
            );
        }
    }
}
