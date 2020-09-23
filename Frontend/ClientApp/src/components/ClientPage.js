import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";

export class ClientPage extends Component {
    static displayName = ClientPage.name;

    constructor(props) {
        super(props);
        this.state = {
            client: {},
            IsLoaded: false,
        };
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
                <div>
                    <div>{client.email}</div>
                    <div>{client.firstName}</div>
                    <div>{client.secondName}</div>
                    <div>{client.lastName}</div>
                    <div>{client.phoneNumber}</div>
                    <div>
                        <select id="accessChange">
                            <option>Admin</option>
                            <option>Moderator</option>
                            <option>User</option>
                        </select>
                    </div>
                </div>
            );
        }
    }
}
