import React, { Component } from 'react'
import { Nav, NavItem } from 'react-bootstrap'

import RegistrationForm from './RegistrationForm'
import LoginForm from './LoginForm'

export default class LoginSection extends Component {
  constructor(props) {
    super(props)

    this.state = {
      login: true
    }
  }

  handleClick() {
    const { login } = this.state
    this.setState( { login: !login } )
  }

  render() {
    const { login } = this.state
    const activeTab = login ? 1 : 2
    return(
      <div className="form-horizontal col-sm-6 col-sm-offset-3 text-center">
        <div style={{marginTop: "10px"}}>
          <Nav bsStyle="tabs" activeKey={ activeTab } onSelect={ () => this.handleClick() } >
            <NavItem eventKey={1} >Login</NavItem>
            <NavItem eventKey={2} >Registration</NavItem>
          </Nav>
        </div>
        { login ? <LoginForm /> : <RegistrationForm /> }
      </div>
    )
  }
}
