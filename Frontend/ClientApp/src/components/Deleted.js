import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";

export class Deleted extends Component {
    static displayName = Deleted.name;

    constructor(props) {
        super(props);

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
        if (this.state.IsLoaded)
            axios.post('https://localhost:5001/api/Product/DeleteProduct', this.state.product);
    }

    render() {
        return (
            <div>
                <strong>Deleted.</strong>
            </div>
        );
    }
}
