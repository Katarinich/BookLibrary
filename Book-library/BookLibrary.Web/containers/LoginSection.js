import React, { Component } from 'react'

import RegistrationForm from './RegistrationForm'

export default class LoginSection extends Component {
  render() {
    return(
      <div>
        <ul className="nav nav-tabs">
          <li role="presentation" className="active"><a href="#">Login</a></li>
          <li role="presentation"><a href="#">Registration</a></li>
        </ul>
        <RegistrationForm />
      </div>
    )
  }
}
