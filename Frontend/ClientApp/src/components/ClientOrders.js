﻿import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import "./ClientOrders.css";

export class ClientOrders extends Component {
    static displayName = ClientOrders.name;

    constructor(props) {
        super(props);
        this.getContent = this.getContent.bind(this);
        this.getPrice = this.getPrice.bind(this);
        this.state = {
            client: {},
            IsLoaded: false
        }
    }

    getContent(e) {
        if (e.target.parentNode.id === "")
            return;
        Array.from(e.target.parentNode.parentNode.childNodes).map(a => {
            a.style.border = "none";
            a.style.padding = "5px";
        });
        e.target.parentNode.style.border = "3px solid black";
        e.target.parentNode.style.padding = "2px";
        let elem = document.getElementById("orderContent");
        elem.innerHTML = "<div><p><b>Product</b></p><p><b>Price</b></p><p><b>Count</b></p></div>";
        let order = this.state.client.orders.find(a => a.id === e.target.parentNode.id);
        order.basket.products.map(a => {
            elem.innerHTML += ("<div><p>" + a.product.name + "</p>"
                + "<p>" + a.product.price + "</p>"
                + "<p>" + a.count + "</p></div>");
        });
    }

    async getPrice(id) {
        let price;
        await axios.get(`https://localhost:5001/api/Order/GetPriceOfOrder/${id}`)
            .then(res => res.data)
            .then((data) => {
                price = data;
            })
            .catch(console.log);
        document.getElementById(id).innerHTML += `<td class="price">${price}</td>`;
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
                            <th class="price">Price</th>
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
                            this.getPrice(order.id);
                            return (
                                <tr id={order.id} onClick={this.getContent}>
                                    <td>{order.id}</td>
                                    <td>{state}</td>
                                    <td>{order.date}</td>
                                </tr>);
                        })}
                    </table>
                    <div id="orderContent">
                    </div>
                </div>
            );
        }
    }
}
