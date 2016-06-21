import React, { Component, PropTypes } from 'react'
import { connect } from 'react-redux'

import { Button, FormGroup, FormControl, ControlLabel } from 'react-bootstrap'

import { loginUser } from '../actions'

class LoginForm extends Component {
  handleSubmit(e) {
    e.preventDefault()

    const { loginUser } = this.props

    var formData = {
      login: $('[name=userName]').val(),
      password: $('[name=password]').val()
    }

    loginUser(formData)
  }

  render() {
    return(
      <form onSubmit={ e => this.handleSubmit(e) } >
        <FormGroup>
          <ControlLabel>Username: </ControlLabel>
          <FormControl type="text" name="userName" defaultValue="Jane.Doe" />
        </FormGroup>
        <FormGroup>
          <ControlLabel>Password: </ControlLabel>
          <FormControl type="password" name="password" defaultValue="12345678"/>
        </FormGroup>
        <Button type="submit">
          Submit
        </Button>
      </form>
    )
  }
}

LoginForm.propTypes = {
  loginUser: PropTypes.func.isRequired
}

export default connect(
  () => ({}),
  { loginUser }
)(LoginForm)
