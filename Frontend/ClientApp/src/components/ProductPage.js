import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import { Basket } from './Basket';
import './ProductPage.css';

export class ProductPage extends Component {
    static displayName = ProductPage.name;

    constructor(props) {
        super(props);
        this.onClickAddToBasket = this.onClickAddToBasket.bind(this);
        this.state = {
            product: {},
            IsLoaded: false,
        };
    }

    async onClickAddToBasket() {
        await axios.post("https://localhost:5001/api/Order/AddProductToOrder/0", this.state.product);
        await this.props.onOrderChange();
    }
    
    async componentDidMount() {
        let url = `https://localhost:5001/api/Product/GetProduct/` + `${this.props.match.params.id}`
        await axios.get(url)
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, product: data })
            })
            .catch(console.log);
    }

    render() {
        var { IsLoaded, product } = this.state;
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
                    <h1>{product.name}</h1>
                    <div><strong>Price</strong>:{product.price}</div>
                    <div><strong>Category</strong>:{product.category}</div>
                    <div><strong>Description</strong>:{product.description}</div>
                    <NavLink tag={Link} id="deleteLink" to={`/deleted${product.id}`}>Delete</NavLink>
                    <p>|</p>
                    <NavLink id="changeLink" tag={Link} to={`/update_product${product.id}`}>Change</NavLink>
                    <p>|</p>
                    <NavLink id="basketLink" onClick={this.onClickAddToBasket} tag={Link}>Add to basket</NavLink>
                </div>
            );
        }
    }
}
