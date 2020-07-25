﻿import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import "./AddProduct.css";

export class AddProduct extends Component {
    static displayName = AddProduct.name;

    constructor(props) {
        super(props);
        this.onClick = this.onClick.bind(this);
    }

    onClick() {
        let product = {
            name: document.getElementById("name").value,
            price: Number(document.getElementById("price").value),
            category: document.getElementById("category").value,
            description: document.getElementById("description").value,
            id: Number(document.getElementById("id").value),
        };
        axios.post('https://localhost:5001/api/Product/AddProduct', product);
    }

    componentDidMount() {
        var btn = document.getElementById("addProduct");
        btn.addEventListener("click", this.onClick);
    }

    render() {
        return (
            <form name="add">
                <input type="number" id="id" placeholder="Id"></input>
                <input type="text" id="name" placeholder="Name"></input>
                <input type="number" id="price" placeholder="Price"></input>
                <input type="text" id="category" placeholder="Category"></input>
                <textarea id="description" placeholder="Description"></textarea>
                <input type="button" id="addProduct" value="Add" />
            </form>
        );
    }
}
