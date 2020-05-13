import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";

function onClick(e) {
    let product = {
        name: document.getElementById("name").value,
        price: Number(document.getElementById("price").value),
        category: document.getElementById("category").value,
        description: document.getElementById("description").value,
        id: Number(document.getElementById("id").value),
    };
    axios.post('https://localhost:5001/api/Product/UpdateProduct', product);
}

export class UpdateProduct extends Component {
    static displayName = UpdateProduct.name;

    constructor(props) {
        super(props);
        this.onClick = onClick;

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
        var btn = document.getElementById("updateProduct");
        btn.addEventListener("click", onClick);
    }

    render() {
        return (
            <form name="update">
                <input type="text" id="name" value={this.state.product.name}></input>
                <input type="number" id="price" value={this.state.product.price}></input>
                <input type="text" id="category" value={this.state.product.category}></input>
                <input type="text" id="description" value={this.state.product.description}></input>
                <input type="hidden" id="id" value={this.state.product.id}></input>
                <input type="button" id="updateProduct" value="Update" />
            </form>
        );
    }
}
