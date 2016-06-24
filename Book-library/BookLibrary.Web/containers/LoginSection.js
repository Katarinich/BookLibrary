import React, { Component } from 'react'
import { Nav, NavItem } from 'react-bootstrap'
import { connect } from 'react-redux'

import RegistrationForm from '../components/RegistrationForm'
import LoginForm from '../components/LoginForm'
import { loginUser, registerUser, initiatePasswordRecovery, hideResponseMessage } from '../actions'

class LoginSection extends Component {
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
    const { location, hideResponseMessage, loginUser, registerUser, initiatePasswordRecovery } = this.props
    const { type, message } = this.props.response
    const activeTab = login ? 1 : 2

    return(
      <div className="form-horizontal col-sm-6 col-sm-offset-3 text-center">
        <div style={{marginTop: "10px"}}>
          <Nav bsStyle="tabs" activeKey={ activeTab } onSelect={ () => this.handleClick() } >
            <NavItem eventKey={1} >Login</NavItem>
            <NavItem eventKey={2} >Registration</NavItem>
          </Nav>
        </div>
        <div style={{paddingTop: '10px'}}>
          { login ? <LoginForm type={ type } message={ message } onSubmitRecovery={ initiatePasswordRecovery } onSubmit={ loginUser } location={ location }/>
                  : <RegistrationForm type={ type } message={ message } onUnmount={ hideResponseMessage } onSubmit={ registerUser }/> }
        </div>
      </div>
    )
  }
}

function mapStateToProps(state) {
  return state
}

export default connect(mapStateToProps, { registerUser, loginUser, hideResponseMessage, initiatePasswordRecovery })(LoginSection)
