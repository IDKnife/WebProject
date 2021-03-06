﻿import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import './Basket.css';

export class Basket extends Component {
    static displayName = Basket.name;

    constructor(props) {
        super(props);
        this.onChange = this.onChange.bind(this);
        this.onClickDelete = this.onClickDelete.bind(this);
        this.toProduct = this.toProduct.bind(this);
        this.Checkout = this.Checkout.bind(this);
        this.updateState = this.updateState.bind(this);
        this.state = {
            order: {},
            IsLoaded: false,
        };
    }

    async Checkout() {
        let order;
        if (sessionStorage.getItem("IsAuthorized") === "true") {
            await axios.get(`https://localhost:5001/api/Order/GetOrder/${sessionStorage.getItem("order_id")}`)
                .then(res => res.data)
                .then((data) => {
                    order = data;
                })
                .catch(console.log);
            await axios.post(`https://localhost:5001/api/Client/AddOrderToList/${sessionStorage.getItem("client_id")}`,
                order, { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } });
            alert(`Order confirmed, now you can check it in your cabinet. Your order id is - ${
                sessionStorage.getItem("order_id")}`);
        } else {
            alert(`Order confirmed. Your order id is - ${
                sessionStorage.getItem("order_id")}`);
        }
        order = {
            clientId: "anonym",
            basket: { products: [] },
            date: new Date(),
            state: 0
        }
        await axios.post(`https://localhost:5001/api/Order/AddOrder`, order)
            .then(res => res.data)
            .then((data) => {
                sessionStorage.setItem("order_id", data);
            })
            .catch(console.log);
        await this.props.onOrderChange();
        window.location.replace(`https://localhost:5011`);
    }

    toProduct(e) {
        let url = "https://localhost:5011/product" + e.target.nextSibling.nextSibling.nextSibling.id;
        window.location.replace(url);
    }

    async onChange(e) {
        await axios.post(`https://localhost:5001/api/Order/${sessionStorage.getItem("order_id")}/UpdateProductCount/${e.target.id.slice(12)}/${+e.target.value}`);
        await this.props.onOrderChange();
    }

    async updateState() {
        let url = `https://localhost:5001/api/Order/GetOrder/${sessionStorage.getItem("order_id")}`;
        await axios.get(url)
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, order: data });
            })
            .catch(console.log);
        await this.props.onOrderChange();
        this.state.order.basket.products.forEach((item) => {
            let elem = document.getElementById("productCount" + item.product.id);
            elem.value = String(item.count);
        });
    }

    async onClickDelete(e) {
        await axios.post(`https://localhost:5001/api/Order/${sessionStorage.getItem("order_id")}/DeleteProduct/${e.target.id}`);
        this.updateState();
    }

    async componentDidMount() {
        this.updateState();
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
            return (
                <div>
                    <table>
                        <tr>
                            <td>
                                Date of creation:
                            </td>
                            <td>
                                {order.date}
                            </td>
                        </tr>
                        <tr class="basketElement">
                            <td>
                                Name
                            </td>
                            <td>
                                Price
                            </td>
                            <td>
                                Count
                            </td>
                        </tr>
                        {order.basket.products.map((element) => (
                            <tr class="basketElement">
                                <td onClick={this.toProduct}>
                                    {element.product.name}
                                </td>
                                <td>
                                    {element.product.price}
                                </td>
                                <td>
                                    <input onChange={this.onChange} class="productCount" id={`productCount${element.product.id}`} type="number" />
                                </td>
                                <td id={element.product.id} onClick={this.onClickDelete}>
                                    Delete
                                </td>
                            </tr>
                        ))}
                    </table>
                    <div class="btn" onClick={this.Checkout}>Checkout</div>
                </div>
            );
        }
    }
}
