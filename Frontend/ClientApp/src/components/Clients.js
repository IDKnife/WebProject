import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import './Clients.css';


export class Clients extends Component {
    static displayName = Clients.name;

    constructor(props) {
        super(props);
        this.AccessConverter = this.AccessConverter.bind(this);
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
    async componentDidMount() {
        await axios.get("https://localhost:5001/api/Client/Clients", { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } })
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
                        {clients.map((client) => (
                            <li class="li-flex">
                                <p>{client.email}</p>
                                <p>{this.AccessConverter(client)}</p>
                            </li>
                        ))}
                    </ul>
                </div>
            );
        }
    }
}
