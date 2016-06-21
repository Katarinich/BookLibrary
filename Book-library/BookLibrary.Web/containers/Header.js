import React, { Component } from 'react'
import { Nav, Navbar, NavItem } from 'react-bootstrap'
import { Link } from 'react-router'
import { LinkContainer } from 'react-router-bootstrap'

export default class Header extends Component {
  render() {
    return(
      <Navbar inverse>
        <Navbar.Header>
            <Navbar.Brand>
              <Link to="/">BookLibrary</Link>
            </Navbar.Brand>
          <Navbar.Toggle />
        </Navbar.Header>
        <Navbar.Collapse >
          <Nav bsStyle="pills" pullRight>
            <LinkContainer to={{ pathname: '/profile/1' }}>
              <NavItem>Ivan Ivanov</NavItem>
            </LinkContainer>
            <LinkContainer to={{ pathname: '/login' }}>
              <NavItem>Log out</NavItem>
            </LinkContainer>
          </Nav>
        </Navbar.Collapse>
      </Navbar>);
  }
}
