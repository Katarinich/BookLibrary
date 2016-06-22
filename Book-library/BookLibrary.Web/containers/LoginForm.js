import React, { Component, PropTypes } from 'react'
import { connect } from 'react-redux'

import { Button, FormGroup, FormControl, ControlLabel } from 'react-bootstrap'

import { loginUser } from '../actions'

class LoginForm extends Component {
  handleSubmit(e) {
    e.preventDefault()

    const { loginUser } = this.props

    var formData = {
      login: $('[name=login]').val(),
      password: $('[name=password]').val()
    }

    loginUser(formData)
  }

  render() {
    return(
      <div style={{paddingTop: '10px'}}>
        <form onSubmit={ e => this.handleSubmit(e) } >
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="login" defaultValue="Jane.Doe" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="password" name="password" defaultValue="12345678"/>
            </div>
          </FormGroup>
          <Button type="submit">
            Submit
          </Button>
        </form>
      </div>
    )
  }
}

LoginForm.propTypes = {
  loginUser: PropTypes.func.isRequired
}

export default connect(() => ({}), { loginUser })(LoginForm)
