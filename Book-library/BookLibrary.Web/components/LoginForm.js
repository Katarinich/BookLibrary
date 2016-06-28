import React, { Component, PropTypes } from 'react'
import { Link } from 'react-router'
import { Button, FormGroup, FormControl, ControlLabel } from 'react-bootstrap'

import HttpResponseMessage from './HttpResponseMessage'

export default class LoginForm extends Component {
  constructor(props) {
    super(props)

    this.state = {
      forgetPassword: false
    }
  }

  componentWillUnmount() {
    if(this.props.type) this.props.onUnmount()
  }

  handleSubmit(e) {
    e.preventDefault()

    const { onSubmit, location, type, message } = this.props

    var formData = {
      login: $('[name=login]').val(),
      password: $('[name=password]').val()
    }

    onSubmit(formData, location)
  }

  handleSubmitPasswordRecoveryForm(e) {
    e.preventDefault()

    const { onSubmitRecovery } = this.props

    var emailValue = $('[name=email]').val()
    onSubmitRecovery(emailValue)
  }

  handleClick() {
    const { forgetPassword } = this.state
    this.setState( {forgetPassword: !forgetPassword} )
  }

  render() {
    const { forgetPassword } = this.state
    const { type, message } = this.props

    if(forgetPassword) {
      return(
        <div>
          { type && <HttpResponseMessage type={ type } message={ message } /> }
          <form onSubmit={ e => this.handleSubmitPasswordRecoveryForm(e) } >
            <h3 style={{marginTop: "10px"}}>Password Recovery</h3>
            <p style={{textAlign: "left"}}>Please, enter your email address:</p>
            <FormGroup bsClass="form-group form-group-lg">
              <div className="col-sm-12">
                <FormControl type="email" name="email" placeholder="Email" />
              </div>
            </FormGroup>
            <FormGroup>
              <div className="col-sm-6" style={{textAlign: 'left'}}>
                <a href="#" onClick={ () => this.handleClick() }>Sign In</a>
              </div>
            </FormGroup>
            <Button bsSize="large" bsStyle="primary" type="submit">
              Send Recovery Code
            </Button>
          </form>
        </div>
      )
    }

    return(
      <div>
        { type && <HttpResponseMessage type={ type } message={ message } /> }
        <form onSubmit={ e => this.handleSubmit(e) } >
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="login" placeholder="Login"/>
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="password" name="password" placeholder="Password"/>
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1" style={{textAlign: 'left'}}>
              <a href="#" onClick={ () => this.handleClick() }>Forget password?</a>
            </div>
          </FormGroup>
          <Button type="submit">
            Sign In
          </Button>
        </form>
      </div>
    )
  }
}
