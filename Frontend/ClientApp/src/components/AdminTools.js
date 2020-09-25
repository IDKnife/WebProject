import React, { Component } from 'react';


export class AdminTools extends Component {
    static displayName = AdminTools.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div id="adminTools">
                <div class="btn" onClick={function () { window.location.replace('https://localhost:5011/clients'); }}>Clients</div>
                <div class="btn" onClick={function () { window.location.replace('https://localhost:5011/orders'); }}>Orders</div>
            </div>
        );
    }
}
