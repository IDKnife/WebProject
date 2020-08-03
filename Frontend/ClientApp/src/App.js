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
            price: 0,
        };
        this.handleOrderChange = this.handleOrderChange.bind(this);
    }

    async handleOrderChange() {
        await axios.get(`https://localhost:5001/api/Order/GetPriceOfOrder/0`)
            .then(res => res.data)
            .then((data) => {
                this.setState({ price: data })
            })
            .catch(console.log);
        document.getElementById("basketCost").innerText = this.state.price.toFixed(2);
    }

    async componentDidMount() {
        await this.handleOrderChange();
    }

    render() {
        return (
            <Layout>
                <Route exact path='/' render={(props) => <MainPage {...props} onOrderChange={this.handleOrderChange} />} />
                <Route path='/search-:name' render={(props) => <MainPage {...props} onOrderChange={this.handleOrderChange} />} />
                <Route path='/category/:category' render={(props) => <MainPage {...props} onOrderChange={this.handleOrderChange} />} />
                <Route path='/add_product' component={AddProduct} />
                <Route path='/info' component={Info} />
                <Route path='/product:id' render={(props) => <ProductPage {...props} onOrderChange={this.handleOrderChange}/>} />
                <Route path='/deleted:id' component={Deleted} />
                <Route path='/update_product:id' component={UpdateProduct} />
                <Route path='/basket' render={(props) => <Basket {...props} onOrderChange={this.handleOrderChange}/>} />
            </Layout>
        );
    }
}
