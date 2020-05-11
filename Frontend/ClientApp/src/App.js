import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { MainPage } from './components/MainPage';
import { FetchData } from './components/FetchData';
import { AddProduct } from './components/AddProduct';
import { ProductPage } from './components/ProductPage';
import { Deleted } from './components/Deleted'

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={MainPage} />
        <Route path='/add_product' component={AddProduct} />
        <Route path='/product:id' component={ProductPage} />
        <Route path='/deleted:id' component={Deleted} />
      </Layout>
    );
  }
}
