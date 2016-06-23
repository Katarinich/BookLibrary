import React, { Component, PropTypes } from 'react'

import { Button, FormGroup, FormControl, ControlLabel } from 'react-bootstrap'

export default class LoginForm extends Component {
  handleSubmit(e) {
    e.preventDefault()

    const { onSubmit, location } = this.props

    var formData = {
      login: $('[name=login]').val(),
      password: $('[name=password]').val()
    }

    onSubmit(formData, location)
  }

  render() {
    return(
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
          Sign In
        </Button>
      </form>
    )
  }
}
