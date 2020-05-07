import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { SearchTab } from './SearchTab';
import { Categories } from './Categories';

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div>
                <Categories />
                <NavMenu />
                <SearchTab />
                <Container>
                    {this.props.children}
                </Container>
            </div>
        );
    }
}
