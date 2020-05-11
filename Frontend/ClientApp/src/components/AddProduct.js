import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";
import "./AddProduct.css";

function onClick(e) {
    let product = {
        name: document.getElementById("name").value,
        price: Number(document.getElementById("price").value),
        category: document.getElementById("category").value,
        description: document.getElementById("description").value,
        id: Number(document.getElementById("id").value),
    };
    axios.post('https://localhost:5001/api/Product/AddProduct', product);
}

export class AddProduct extends Component {
    static displayName = AddProduct.name;

    constructor(props) {
        super(props);
        this.onClick = onClick;
    }

    

    componentDidMount() {
        var btn = document.getElementById("addProduct");
        btn.addEventListener("click", onClick);
    }

render() {
        return (
            <form name="add">
                <input type="text" id="name" placeholder="Name"></input>
                <input type="number" id="price" placeholder="Price"></input>
                <input type="text" id="category" placeholder="Category"></input>
                <input type="text" id="description" placeholder="Description"></input>
                <input type="number" id="id" placeholder="Id"></input>
                <input type="button" id="addProduct" value="Add" />
            </form>
        );
}
}
