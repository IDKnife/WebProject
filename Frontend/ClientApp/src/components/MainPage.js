﻿import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import './MainPage.css';

export class MainPage extends Component {
    static displayName = MainPage.name;

    constructor(props) {
        super(props);
        this.onClickAscendingName = this.onClickAscendingName.bind(this);
        this.onClickDescendingName = this.onClickDescendingName.bind(this);
        this.onClickAscendingPrice = this.onClickAscendingPrice.bind(this);
        this.onClickDescendingPrice = this.onClickDescendingPrice.bind(this);
        this.state = {
            products: [],
            IsLoaded: false,
        };

    }

    async componentDidMount() {
        let url;
        if (this.props.match.params.name != null)
            url = `https://localhost:5001/api/Product/Products/` + `${this.props.match.params.name}`;
        else if (this.props.match.params.category != null)
            url = `https://localhost:5001/api/Product/Products/-/` + `${this.props.match.params.category}`;
        else
            url = `https://localhost:5001/api/Product/Products`;
        await axios.get(url)
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, products: data })
            })
            .catch(console.log);
    }

    onClickAscendingName() {
        this.setState({
            IsLoaded: true, products: this.state.products.sort(function (a, b) {
                if (a.name > b.name) return 1;
                if (a.name == b.name) return 0;
                if (a.name < b.name) return -1;
            })
        })
    }

    onClickDescendingName() {
        this.setState({
            IsLoaded: true, products: this.state.products.sort(function (a, b) {
                if (a.name > b.name) return -1;
                if (a.name == b.name) return 0;
                if (a.name < b.name) return 1;
            })
        })
    }

    onClickAscendingPrice() {
        this.setState({
            IsLoaded: true, products: this.state.products.sort(function (a, b) {
                if (a.price > b.price) return 1;
                if (a.price == b.price) return 0;
                if (a.price < b.price) return -1;
            })
        })
    }

    onClickDescendingPrice() {
        this.setState({
            IsLoaded: true, products: this.state.products.sort(function (a, b) {
                if (a.price > b.price) return -1;
                if (a.price == b.price) return 0;
                if (a.price < b.price) return 1;
            })
        })
    }

    render() {
        var { IsLoaded, products } = this.state;
        let i = 0;
        let k = 0;
        let parts = [];
        if (IsLoaded)
            while (i <= products.length) {
                parts.push(products.slice(i, i + 4));
                i += 4;
                k++;
            }
        if (!IsLoaded) {
            return (
                <p>
                    Loading.
                </p>
            );
        }
        else {
            return (
                <table>
                    <tr>
                        <p><b>Name:</b></p><th onClick={this.onClickAscendingName}>Ascending</th><th onClick={this.onClickDescendingName}>Descending</th>
                        <p><b>Price:</b></p><th onClick={this.onClickAscendingPrice}>Ascending</th><th onClick={this.onClickDescendingPrice}>Descending</th>
                    </tr>
                    {parts.map((products) => (
                        <tr>
                            {products.map((product) => (
                                <NavLink tag={Link} to={`/product${product.id}`} >
                                    <td>
                                        <div>{product.name}</div>
                                        <div>{product.description}</div>
                                        <div>{product.price}</div>
                                    </td>
                                </NavLink>
                            ))}
                        </tr>
                    ))}
                </table>
            );
        }
    }
}