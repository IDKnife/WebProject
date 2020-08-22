import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import "./ClientOrders.css";

export class ClientOrders extends Component {
    static displayName = ClientOrders.name;

    constructor(props) {
        super(props);
        this.state = {
            client: {},
            IsLoaded: false
        }
    }

    async componentDidMount() {
        await axios.get(`https://localhost:5001/api/Client/GetClient/${sessionStorage.getItem("client_id")}`,
            { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } })
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, client: data })
            })
            .catch(console.log);
    }

    render() {
        let { IsLoaded, client } = this.state;
        if (!IsLoaded) {
            return (
                <p>
                    Loading.
                </p>
            );
        } else {
            return (
                <div id="clientOrdersContainer">
                    <h3>Orders</h3>
                    <table>
                        <tr>
                            <th>Id</th>
                            <th>State</th>
                            <th>Date</th>
                        </tr>
                        {client.orders.map((order) => {
                            let state;
                            switch (order.state) {
                                case 0:
                                    state = "Forming";
                                    break;
                                case 1:
                                    state = "Payed";
                                    break;
                                case 2:
                                    state = "ReadyForDelievering";
                                    break;
                                case 3:
                                    state = "Delievered";
                                    break;
                            }
                            return (
                                <tr id={order.id}>
                                    <td>{order.id}</td>
                                    <td>{state}</td>
                                    <td>{order.date}</td>
                                </tr>);
                        })}
                    </table>
                </div>
            );
        }
    }
}
