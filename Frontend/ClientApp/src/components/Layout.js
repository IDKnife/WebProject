import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { SearchTab } from './SearchTab';
import { Categories } from './Categories';
import './Layout.css';

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div>
                <Categories />
                <div id="top" >
                    <NavMenu />
                    <SearchTab />
                    <div id="container">
                        <Container>
                            {this.props.children}
                        </Container>
                    </div>
                </div>
            </div>
        );
    }
}
