import React, { Component } from 'react';

export class Info extends Component {
    static displayName = Info.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <div>
                    <p><strong>Author: </strong></p><p>Arkhincheev S.B.</p>
                </div>
                <div>
                    <p><strong>Email: </strong></p><p>IDKnife@mail.ru</p>
                </div>
                <div>
                    <p>
                        <strong>Fraimworks: </strong><br></br>
                        <i>Client</i> - React.js;<br></br>
                        <i>Server</i> - ASP.Net Core 3.1;<br></br>
                        <i>Database</i> - MongoDB;<br></br>
                    </p>
                </div>
            </div>
        );
    }
}
