import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";

export class OrderPage extends Component {
    static displayName = OrderPage.name;

    constructor(props) {
        super(props);
        this.getPrice = this.getPrice.bind(this);
        this.updateOrderState = this.updateOrderState.bind(this);
        this.deleteOrder = this.deleteOrder.bind(this);
        this.state = {
            order: {},
            IsLoaded: false,
        };
    }

    async updateOrderState() {
        this.state.order.state = document.getElementById("stateChange").selectedIndex;
        await axios.post(`https://localhost:5001/api/Order/UpdateOrder`,
            this.state.order,
            { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } });
    }

    async deleteOrder() {
        await axios.post(`https://localhost:5001/api/Order/DeleteOrder/${this.state.order.id}`,
            {},
            { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } });
        window.location.replace(`https://localhost:5011/orders`);
    }

    async componentDidMount() {
        await axios.get(`https://localhost:5001/api/Order/GetOrder/${this.props.match.params.id}`)
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, order: data });
                let elem = document.getElementById("stateChange");
                elem.options.selectedIndex = this.state.order.state;
                if (sessionStorage.getItem("access_level") !== "Admin") {
                    let i = 3;
                    while (i > this.state.order.state) {
                        elem.remove(i);
                        i--;
                    }
                    i--;
                    while (i > -1) {
                        elem.remove(i);
                        i--;
                    }
                }
            })
            .catch(console.log);
    }

    async getPrice() {
        let price;
        await axios.get(`https://localhost:5001/api/Order/GetPriceOfOrder/${this.props.match.params.id}`)
            .then(res => res.data)
            .then((data) => {
                price = data;
            })
            .catch(console.log);
        document.getElementById("price").innerText = price.toFixed(2);
    }

    render() {
        var { IsLoaded, order } = this.state;
        if (!IsLoaded) {
            return (
                <p>
                    Loading.
                </p>
            );
        }
        else {
            this.getPrice();
            return (
                <div class="page">
                    <div class="innerPage">
                        <div><p>Client Id: </p><p>{order.clientId}</p></div>
                        <div><p>Date: </p><p>{order.date}</p></div>
                        <div><p>Price: </p><p id="price"></p></div>
                        <div>
                            <select onChange={this.updateOrderState} id="stateChange">
                                <option>Forming</option>
                                <option>Payed</option>
                                <option>ReadyForDelievering</option>
                                <option>Delievered</option>
                            </select>
                            <p class="btn" onClick = { this.deleteOrder }>Delete</p>
                        </div>
                    </div>
                    <div class="innerPage">
                        <div>Basket:</div>
                        <div class="basket">
                            <p>Name:</p>
                            <p>Price:</p>
                            <p>Count:</p>
                        </div>
                        {order.basket.products.map((position) => (
                            <div class="basket">
                                <p>{position.product.name}</p>
                                <p>{position.product.price}</p>
                                <p>{position.count}</p>
                            </div>
                        ))}
                    </div>
                </div>
            );
        }
    }
}
