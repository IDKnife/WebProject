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
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={MainPage} />
        <Route path='/search-:name' component={MainPage} />
        <Route path='/category/:category' component={MainPage} />
        <Route path='/add_product' component={AddProduct} />
        <Route path='/info' component={Info} />
        <Route path='/product:id' component={ProductPage} />
        <Route path='/deleted:id' component={Deleted} />
        <Route path='/update_product:id' component={UpdateProduct} />
        <Route path='/basket' component={Basket} />
      </Layout>
    );
  }
}
