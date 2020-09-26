import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";


export class AdminViewOnOrders extends Component {
    static displayName = AdminViewOnOrders.name;

    constructor(props) {
        super(props);
        this.StateConverter = this.StateConverter.bind(this);
        this.sortAscendingClientId = this.sortAscendingClientId.bind(this);
        this.sortDescendingClientId = this.sortDescendingClientId.bind(this);
        this.sortAscendingState = this.sortAscendingState.bind(this);
        this.sortDescendingState = this.sortDescendingState.bind(this);
        this.sortAscendingDate = this.sortAscendingDate.bind(this);
        this.sortDescendingDate = this.sortDescendingDate.bind(this);
        this.state = {
            orders: {},
            IsLoaded: false
        };
    }
    StateConverter(order) {
        switch (order.state) {
            case 0:
                return ("Forming");
            case 1:
                return ("Payed");
            case 2:
                return ("ReadyForDelievering");
            case 3:
                return ("Delievered");
        }
    }
    sortAscendingClientId() {
        this.setState({
            orders: this.state.orders.sort(function (a, b) {
                if (a.clientId > b.clientId) return 1;
                if (a.clientId == b.clientId) return 0;
                if (a.clientId < b.clientId) return -1;
            })
        })
    }
    sortDescendingClientId() {
        this.setState({
            orders: this.state.orders.sort(function (a, b) {
                if (a.clientId > b.clientId) return -1;
                if (a.clientId == b.clientId) return 0;
                if (a.clientId < b.clientId) return 1;
            })
        })
    }
    sortAscendingState() {
        this.setState({
            orders: this.state.orders.sort(function (a, b) {
                if (a.state > b.state) return 1;
                if (a.state == b.state) return 0;
                if (a.state < b.state) return -1;
            })
        })
    }
    sortDescendingState() {
        this.setState({
            orders: this.state.orders.sort(function (a, b) {
                if (a.state > b.state) return -1;
                if (a.state == b.state) return 0;
                if (a.state < b.state) return 1;
            })
        })
    }
    sortAscendingDate() {
        this.setState({
            orders: this.state.orders.sort(function (a, b) {
                if (new Date(a.date) > new Date(b.date)) return 1;
                if (new Date(a.date) == new Date(b.date)) return 0;
                if (new Date(a.date) < new Date(b.date)) return -1;
            })
        })
    }
    sortDescendingDate() {
        this.setState({
            orders: this.state.orders.sort(function (a, b) {
                if (new Date(a.date) > new Date(b.date)) return -1;
                if (new Date(a.date) == new Date(b.date)) return 0;
                if (new Date(a.date) < new Date(b.date)) return 1;
            })
        })
    }
    async componentDidMount() {
        await axios.get("https://localhost:5001/api/Order/Orders",
            { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } })
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, orders: data })
            })
            .catch(console.log);
    }

    render() {
        var { IsLoaded, orders } = this.state;
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
                        <li class="li-flex orders">
                            <p>
                                <b>Client Id  </b>
                                <img onClick={this.sortAscendingClientId} class="sort" src="./images/up.png" />
                                <img onClick={this.sortDescendingClientId} class="sort" src="./images/down.png" />
                            </p>
                            <p>
                                <b>State  </b>
                                <img onClick={this.sortAscendingState} class="sort" src="./images/up.png" />
                                <img onClick={this.sortDescendingState} class="sort" src="./images/down.png" />
                            </p>
                            <p>
                                <b>Date  </b>
                                <img onClick={this.sortAscendingDate} class="sort" src="./images/up.png" />
                                <img onClick={this.sortDescendingDate} class="sort" src="./images/down.png" />
                            </p>
                        </li>
                        {orders.map((order) => (
                            <li onClick={function () { window.location.replace(`https://localhost:5011/order_page${order.id}`)  }} class="li-flex orders">
                                <p>{order.clientId}</p>
                                <p>{this.StateConverter(order)}</p>
                                <p>{order.date}</p>
                            </li>
                        ))}
                    </ul>
                </div >
            );
        }
    }
}
