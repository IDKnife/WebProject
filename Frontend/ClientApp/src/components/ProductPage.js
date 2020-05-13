import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import './ProductPage.css';

export class ProductPage extends Component {
    static displayName = ProductPage.name;

    constructor(props) {
        super(props);

        this.state = {
            product: {},
            IsLoaded: false,
        };
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
                    <NavLink tag={Link} to={`/deleted${product.id}`}>Delete</NavLink><p>|</p><NavLink tag={Link} to={`/update_product${product.id}` }>Change</NavLink>
                </div>
            );
        }
    }
}
