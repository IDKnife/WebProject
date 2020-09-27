import React, { Component } from 'react';

export class Delivery extends Component {
    static displayName = Delivery.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <div>
                    <p><strong>Within the city: </strong></p><p>0.03$</p>
                </div>
                <div>
                    <p><strong>Outside the city: </strong></p><p>0.07$</p>
                </div>
            </div>
        );
    }
}
