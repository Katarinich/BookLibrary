import React, { Component } from 'react';
import { Nav } from 'react-bootstrap';
import { Link} from 'react-router';
import { Navbar} from 'react-bootstrap';
import { NavItem} from 'react-bootstrap';

export default class Header extends Component {

handleSelect(selectedKey) {
  alert('selected ' + selectedKey);
}

  render() {
    return(
      <Navbar inverse>
      <Navbar.Collapse >
        <Nav bsStyle="pills" pullRight>
        <NavItem> <Link to='/profile/1'>Ivan Ivanov</Link></NavItem>
        <NavItem><Link to='/login'>Log out</Link></NavItem>
        </Nav>
        {this.props.children}
      </Navbar.Collapse>
      </Navbar>);
}
}
