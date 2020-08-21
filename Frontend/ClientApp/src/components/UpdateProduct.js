import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";


export class UpdateProduct extends Component {
    static displayName = UpdateProduct.name;

    constructor(props) {
        super(props);
        this.onClick = this.onClick.bind(this);

        this.state = {
            product: {},
            IsLoaded: false,
        };
    }

    onClick() {
        let product = {
            name: document.getElementById("name").value,
            price: Number(document.getElementById("price").value),
            category: document.getElementById("category").value,
            description: document.getElementById("description").value,
            id: document.getElementById("id").value,
        };
        axios.post('https://localhost:5001/api/Product/UpdateProduct', product, { headers: { 'Authorization': `Bearer ${sessionStorage.getItem("access_token")}` } });
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
        btn.addEventListener("click", this.onClick);
        document.getElementById("name").value = this.state.product.name;
        document.getElementById("price").value = this.state.product.price;
        document.getElementById("category").value = this.state.product.category;
        document.getElementById("description").value = this.state.product.description;
        document.getElementById("id").value = this.state.product.id;
    }

    render() {
        return (
            <form name="update">
                <input type="text" id="name"></input>
                <input type="number" id="price"></input>
                <input type="text" id="category"></input>
                <textarea id="description"></textarea>
                <input type="hidden" id="id"></input>
                <input type="button" id="updateProduct" value="Update" />
            </form>
        );
    }
}
