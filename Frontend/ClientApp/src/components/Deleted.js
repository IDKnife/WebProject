﻿import React, { Component } from 'react';
import axios from "C:/Users/ArhiS/node_modules/axios";

export class Deleted extends Component {
    static displayName = Deleted.name;

    constructor(props) {
        super(props);
    }

    async componentDidMount() {
        let id = Number(this.props.match.params.id);
        axios.post(`https://localhost:5001/api/Product/DeleteProduct`, id, { headers: { 'Content-Type': 'application/json' } });
    }

    render() {
        return (
            <div>
                <strong>Deleted.</strong>
            </div>
        );
    }
}
