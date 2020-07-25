import React, { Component } from 'react';
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
        this.state = {
            order: {},
            IsLoaded: false,
        };
    }

    onChange(e) {
        axios.post(`https://localhost:5001/api/Order/UpdateProductCountInBasket/0`, [+e.target.id.slice(12), +e.target.value], { headers: { 'Content-Type': 'application/json' } });
    }

    onClickDelete(e) {
        axios.post(`https://localhost:5001/api/Order/DeleteProductFromBasket/0`, e.target.id, { headers: { 'Content-Type': 'application/json' } });
        let index = this.state.order.basket.products.findIndex((item) => (item.product.id == e.target.id));
        let item = this.state.order.basket.products.find((item) => (item.product.id == e.target.id));
        this.state.order.basket.products.splice(index, 1);
        this.setState({ IsLoaded: true, order: this.state.order });
        let elem = document.getElementById("basketCost");
        let cost = +elem.innerText - item.product.price;
        elem.innerText = cost.toFixed(2);
    }

    async componentDidMount() {
        let url = `https://localhost:5001/api/Order/GetOrder/0`;
        await axios.get(url)
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, order: data })
            })
            .catch(console.log);
        this.state.order.basket.products.forEach((item) => {
            let elem = document.getElementById("productCount" + item.product.id);
            elem.value = String(item.count);
        });
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
                                <td>
                                    {element.product.name}
                                </td>
                                <td>
                                    {element.product.price}
                                </td>
                                <td>
                                    <input onChange={this.onChange} class="productCount" id={`productCount${element.product.id}`} type="number"/>
                                </td>
                                <td id={element.product.id} onClick={this.onClickDelete}>
                                    Delete
                                </td>
                            </tr>
                        ))}
                    </table>
                </div>
            );
        }
    }
}
