import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
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
                        {products.map((product) => (
                            <td>
                                <div>{product.name}</div>
                                <div>{product.description}</div>
                                <div>{product.price}</div>
                            </td>
                        ))}
                    </tr>
                </table>
            );
        }
    }
}
