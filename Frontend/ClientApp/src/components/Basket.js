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
        this.toProduct = this.toProduct.bind(this);
    }

    toProduct(e) {
        let url = "https://localhost:5011/product" + e.target.nextSibling.nextSibling.nextSibling.id;
        window.location.href = url;
    }

    onChange(e) {
        axios.post(`https://localhost:5001/api/Order/UpdateProductCountInBasket/0`, [+e.target.id.slice(12), +e.target.value], { headers: { 'Content-Type': 'application/json' } });
        this.props.onOrderChange();
    }

    onClickDelete(e) {
        axios.post(`https://localhost:5001/api/Order/DeleteProductFromBasket/0`, e.target.id, { headers: { 'Content-Type': 'application/json' } });
        this.props.onOrderChange();
    }

    async componentDidMount() {
        this.props.order.basket.products.forEach((item) => {
            let elem = document.getElementById("productCount" + item.product.id);
            elem.value = String(item.count);
        });
    }

    render() {
        let IsLoaded = this.props.IsLoaded;
        let order = this.props.order;
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
