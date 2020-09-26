import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { LogIn } from './components/LogIn';
import { SignIn } from './components/SignIn';
import { MainPage } from './components/MainPage';
import { AddProduct } from './components/AddProduct';
import { ProductPage } from './components/ProductPage';
import { Deleted } from './components/Deleted';
import { UpdateProduct } from './components/UpdateProduct';
import { Info } from './components/Info';
import { Basket } from './components/Basket';
import { Cabinet } from './components/Cabinet';
import { ClientOrders } from './components/ClientOrders';
import { AdminTools } from './components/AdminTools';
import { Clients } from './components/Clients';
import { ClientPage } from './components/ClientPage';
import { AdminViewOnOrders } from './components/AdminViewOnOrders';
import { OrderPage } from './components/OrderPage';
import axios from "C:/Users/ArhiS/node_modules/axios";
import './custom.css';

export default class App extends Component {
    static displayName = App.name;
    constructor(props) {
        super(props);
        this.state = {
            price: 0
        };
        this.handleOrderChange = this.handleOrderChange.bind(this);
        this.handleClientLogin = this.handleClientLogin.bind(this);
    }

    async handleClientLogin(email, password) {
        await axios.post(`https://localhost:5021/api/Token/GetToken`, { "password": password, "email": email }, { headers: { 'Content-Type': 'application/json' } })
            .then(res => res.data)
            .then((data) => {
                sessionStorage.setItem("client_id", data._id);
                sessionStorage.setItem("access_token", data.token);
                sessionStorage.setItem("access_level", data.access);
                sessionStorage.setItem("IsAuthorized", true);
            })
            .catch(er => alert(er.response.data.errorText));
        if (sessionStorage.getItem("IsAuthorized") === "true") {
            document.getElementById("User").style.display = "block";
            document.getElementById("Auth").style.display = "none";
            if (sessionStorage.getItem("access_level") !== "User") {
                if (sessionStorage.getItem("access_level") === "Admin")
                    document.getElementById("Admin").style.display = "block";
                document.getElementById("AddProduct").style.display = "block";
            }
            window.location.replace('https://localhost:5011/');
        }
    }

    async handleOrderChange() {
        await axios.get(`https://localhost:5001/api/Order/GetPriceOfOrder/${sessionStorage.getItem("order_id")}`)
            .then(res => res.data)
            .then((data) => {
                this.setState({ price: data })
            })
            .catch(console.log);
        document.getElementById("basketCost").innerText = this.state.price.toFixed(2);
    }

    async componentDidMount() {
        await this.handleOrderChange();
        document.getElementById("Admin").style.display = "none";
        document.getElementById("AddProduct").style.display = "none";
        if (!sessionStorage.getItem("IsAuthorized")) {
            document.getElementById("User").style.display = "none";
        } else {
            document.getElementById("Auth").style.display = "none";
            if (sessionStorage.getItem("access_level") !== "User") {
                if (sessionStorage.getItem("access_level") === "Admin")
                    document.getElementById("Admin").style.display = "block";
                document.getElementById("AddProduct").style.display = "block";
            }
        }
        let order = {
            clientId: "anonym",
            basket: { products: [] },
            date: new Date(),
            state: 0
        }
        if (!sessionStorage.getItem("order_id"))
            await axios.post(`https://localhost:5001/api/Order/AddOrder`, order)
                .then(res => res.data)
                .then((data) => {
                    sessionStorage.setItem("order_id", data);
                })
                .catch(console.log);
    }

    render() {
        return (
            <Layout>
                <Route exact path='/' render={(props) => <MainPage {...props} onOrderChange={this.handleOrderChange} />} />
                <Route path='/search-:name' render={(props) => <MainPage {...props} onOrderChange={this.handleOrderChange} />} />
                <Route path='/category/:category' render={(props) => <MainPage {...props} onOrderChange={this.handleOrderChange} />} />
                <Route path='/add_product' component={AddProduct} />
                <Route path='/info' component={Info} />
                <Route path='/log_in' render={(props) => <LogIn {...props} onClientLogin={this.handleClientLogin} />} />
                <Route path='/sign_in' component={SignIn} />
                <Route path='/product:id' render={(props) => <ProductPage {...props} onOrderChange={this.handleOrderChange} />} />
                <Route path='/deleted:id' component={Deleted} />
                <Route path='/update_product:id' component={UpdateProduct} />
                <Route path='/basket' render={(props) => <Basket {...props} onOrderChange={this.handleOrderChange} />} />
                <Route path='/cabinet' component={Cabinet} />
                <Route path='/client_orders' component={ClientOrders} />
                <Route path='/admin_tools' component={AdminTools} />
                <Route path='/clients' component={Clients} />
                <Route path='/client_page:id' component={ClientPage} />
                <Route path='/orders' component={AdminViewOnOrders} />
                <Route path='/order_page:id' component={OrderPage} />
            </Layout>
        );
    }
}
