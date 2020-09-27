import React, { Component } from 'react';

export class StoreAddresses extends Component {
    static displayName = StoreAddresses.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <div>
                    <p><strong>First address</strong></p>
                </div>
                <div>
                    <p><strong>Second address</strong></p>
                </div>
            </div>
        );
    }
}
