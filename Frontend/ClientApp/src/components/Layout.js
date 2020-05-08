import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { SearchTab } from './SearchTab';
import { Categories } from './Categories';
import { MainPage } from './MainPage';
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
                        <MainPage />
                    </div>
                </div>
            </div>
        );
    }
}
