import React, { Component } from 'react';

export class Feedback extends Component {
    static displayName = Feedback.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <div>
                    <p><strong>If you want to give us some feedback there are some options</strong></p>
                </div>
                <div>
                    <p>EmailForFeedback@gmail.com</p>
                </div>
                <div>
                    <p>PhoneForFeedback</p>
                </div>
                <div>
                    <p>SocialMediaForFeedback</p>
                </div>
            </div>
        );
    }
}
