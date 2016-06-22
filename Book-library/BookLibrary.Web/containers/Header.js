import React, { Component } from 'react'
import { connect } from 'react-redux'
import { Nav, Navbar, NavItem } from 'react-bootstrap'
import { Link } from 'react-router'
import { LinkContainer } from 'react-router-bootstrap'

import { logoutUser } from '../actions'

class Header extends Component {
  handleClick() {
    const { logoutUser, user } = this.props

    logoutUser(user)
  }

  render() {
    const { firstName, lastName, id } = this.props.user
    const { user } = this.props

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
            <LinkContainer to={{ pathname: `/profile/${id}` }}>
              <NavItem>{ firstName } { lastName }</NavItem>
            </LinkContainer>
            <LinkContainer to={{ pathname: '/login' }}>
              <NavItem onClick={ () => this.handleClick() }>Log out</NavItem>
            </LinkContainer>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    )
  }
}

export default connect(() => ({}), { logoutUser })(Header)
