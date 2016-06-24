import React, { Component, PropTypes } from 'react'
import { Link } from 'react-router'
import { Button, FormGroup, FormControl, ControlLabel } from 'react-bootstrap'

export default class LoginForm extends Component {
  constructor(props) {
    super(props)

    this.state = {
      forgetPassword: false
    }
  }

  handleSubmit(e) {
    e.preventDefault()

    const { onSubmit, location } = this.props

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

    if(forgetPassword) {
      return(
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
      )
    }

    return(
      <form onSubmit={ e => this.handleSubmit(e) } >
        <FormGroup>
          <div className="col-sm-10 col-sm-offset-1">
            <FormControl type="text" name="login" defaultValue="Jane.Doe" />
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
    )
  }
}
