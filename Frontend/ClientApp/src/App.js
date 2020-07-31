import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { MainPage } from './components/MainPage';
import { AddProduct } from './components/AddProduct';
import { ProductPage } from './components/ProductPage';
import { Deleted } from './components/Deleted'
import { UpdateProduct } from './components/UpdateProduct'
import { Info } from './components/Info'
import { Basket } from './components/Basket'
import axios from "C:/Users/ArhiS/node_modules/axios";
import './custom.css'

export default class App extends Component {
    static displayName = App.name;
    constructor(props) {
        super(props);
        this.state = {
            order: {},
            IsLoaded: false,
        };
        this.handleOrderChange = this.handleOrderChange.bind(this);
        this.calculateBasketPrice = this.calculateBasketPrice.bind(this);
    }

    calculateBasketPrice() {
        let price = 0;
        this.state.order.basket.products.forEach((item) => {
            price += item.product.price*item.count;
        });
        document.getElementById("basketCost").innerText = price.toFixed(2);
        this.setState({ order: this.state.order, IsLoaded: this.state.IsLoaded });
    }

    handleOrderChange() {
        let url = `https://localhost:5001/api/Order/GetOrder/0`;
        axios.get(url)
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, order: data })
            })
            .catch(console.log);
        this.calculateBasketPrice();
    }

    async componentDidMount() {
        let url = `https://localhost:5001/api/Order/GetOrder/0`;
        await axios.get(url)
            .then(res => res.data)
            .then((data) => {
                this.setState({ IsLoaded: true, order: data })
            })
            .catch(console.log);
        await this.calculateBasketPrice();
    }

    render() {
        return (
            <Layout order={this.state.order} onOrderChange={this.handleOrderChange}>
                <Route exact path='/' component={MainPage} />
                <Route path='/search-:name' component={MainPage} />
                <Route path='/category/:category' component={MainPage} />
                <Route path='/add_product' component={AddProduct} />
                <Route path='/info' component={Info} />
                <Route path='/product:id' render={(props) => <ProductPage {...props} order={this.state.order} onOrderChange={this.handleOrderChange} />} />
                <Route path='/deleted:id' component={Deleted} />
                <Route path='/update_product:id' component={UpdateProduct} />
                <Route path='/basket' render={(props) => <Basket {...props} order={this.state.order} onOrderChange={this.handleOrderChange} IsLoaded={this.state.IsLoaded} />} />
            </Layout>
        );
    }
}
