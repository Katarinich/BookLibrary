import React, { Component, PropTypes } from 'react'
import { Button, FormGroup, FormControl, ControlLabel } from 'react-bootstrap'

import HttpResponseMessage from './HttpResponseMessage'

export default class RegistrationForm extends Component {
  handleSubmit(e) {
    e.preventDefault()

    const { onSubmit } = this.props

    var formData = {
      userName: $('[name=userName]').val(),
      firstName: $('[name=firstName]').val(),
      lastName: $('[name=lastName]').val(),
      email: $('[name=email]').val(),
      dateOfBirth: new Date($('[name=bday]').val()).getTime() / 1000,
      mobilePhone: $('[name=phone]').val(),
      country: $('[name=country]').val(),
      state: $('[name=state]').val(),
      city: $('[name=city]').val(),
      addressLine: $('[name=address]').val(),
      zipcode: $('[name=zipcode]').val(),
      password: $('[name=password]').val()
    }

    onSubmit(formData)
  }

  componentWillUnmount() {
    if(this.props.type) this.props.onUnmount()
  }

  render() {
    const { type, message } = this.props

    return(
      <div>
        { type && <HttpResponseMessage type={ type } message={ message } /> }

        <form onSubmit={ e => this.handleSubmit(e) } >
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="userName" defaultValue="Jane.Doe" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="firstName" defaultValue="Jane" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="lastName" defaultValue="Doe" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="email" name="email" defaultValue="jane.doe@example.com" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="date" name="bday" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="phone" defaultValue="123456789" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="country" defaultValue="Belarus" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="state" defaultValue="None" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="city" defaultValue="Grodno" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="address" defaultValue="qwerty" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="zipcode" defaultValue="123456" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="password" name="password" defaultValue="12345678" />
            </div>
          </FormGroup>
          <Button type="submit">
            Sign Up
          </Button>
        </form>
      </div>
    )
  }
}
