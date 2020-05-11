import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import './MainPage.css';

export class MainPage extends Component {
    static displayName = MainPage.name;

    constructor(props) {
        super(props);

        this.state = {
            products: [],
            IsLoaded: false,
        };
    }

    async componentDidMount() {
        await axios.get('https://localhost:5001/api/Product/Products')
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, products: data })
            })
            .catch(console.log);
    }

    render() {
        var { IsLoaded, products } = this.state;
        let i = 0;
        let k = 0;
        let parts = [];
        if(IsLoaded)
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
                    {parts.map((products) => (
                        <tr>
                            {products.map((product) => (
                                <NavLink tag= { Link } to= { `/product${product.id}` } >
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
