import React, { Component } from 'react';
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
        this.AddToBasket = this.AddToBasket.bind(this);
        this.state = {
            products: [],
            IsLoaded: false,
        };

    }

    async AddToBasket(e) {
        let product = this.state.products.find(item => item.id == e.target.previousSibling.href.slice(30));
        await axios.post(`https://localhost:5001/api/Order/AddProductToOrder/${sessionStorage.getItem("order_id")}`, product);
        await this.props.onOrderChange();
    }

    async componentDidMount() {
        let url;
        if (this.props.match.params.name != null)
            url = `https://localhost:5001/api/Product/Products/` + `${this.props.match.params.name}`;
        else if (this.props.match.params.category != null)
            url = `https://localhost:5001/api/Product/Products/-/` + `${this.props.match.params.category}`;
        else
            url = `https://localhost:5001/api/Product/Products`;
        await axios.get(url/*, { headers: { 'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBY2Nlc3MiOiJVc2VyIiwiSWQiOiI1ZjM3YTQyMGMyN2YwNzVlZmU5YzY4YTEiLCJFbWFpbCI6InNvbWVfZW1haWxAbWFpbC5ydSJ9.XFcRccgIKyS7Wt3DSNIfTxzDKyUXJ_HVkQhvLQ8vNVo' } }*/)
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
                        <p><b>Name:</b></p>
                        <th onClick={this.onClickAscendingName}>
                            <img class="sort" src="./images/up.png" />
                        </th>
                        <th onClick={this.onClickDescendingName}>
                            <img class="sort" src="./images/down.png" />
                        </th>
                        <p><b>Price:</b></p>
                        <th onClick={this.onClickAscendingPrice}>
                            <img class="sort" src="./images/up.png" />
                        </th>
                        <th onClick={this.onClickDescendingPrice}>
                            <img class="sort" src="./images/down.png" />
                        </th>
                    </tr>
                    {parts.map((products) => (
                        <tr>
                            {products.map((product) => (
                                <td class="mainPageProducts">
                                    <NavLink tag={Link} to={`/product${product.id}`} >
                                        <div class="mainPageProduct">
                                            <div>{product.name}</div>
                                            <div>{product.description}</div>
                                            <div>{product.price}</div>
                                        </div>
                                    </NavLink>
                                    <img class="img-btn" src="./images/basket.png" onClick={this.AddToBasket} />
                                </td>
                            ))}
                        </tr>
                    ))}
                </table>
            );
        }
    }
}
