import React, { Component, PropTypes } from 'react'
import { connect } from 'react-redux'
import { Button, FormGroup, FormControl, ControlLabel } from 'react-bootstrap'

import UserEmailChanger from './UserEmailChanger'

export default class EditGeneralInformation extends Component {
  handleSubmit(e) {
    e.preventDefault()

    const { onSubmit, user } = this.props

    var formData = {
      email: user.email,
      userName: $('[name=userName]').val(),
      firstName: $('[name=firstName]').val(),
      lastName: $('[name=lastName]').val(),
      dateOfBirth: new Date($('[name=bday]').val()).getTime() / 1000,
      mobilePhone: $('[name=mobilePhone]').val(),
      country: $('[name=country]').val(),
      state: $('[name=state]').val(),
      city: $('[name=city]').val(),
      addressLine: $('[name=address]').val(),
      zipcode: $('[name=zipcode]').val(),
      password: "12345678"
    }

    onSubmit(formData)
  }

  render() {
    const { user, hideResponseMessage } = this.props
    const options = {
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    }

    return(
      <form onSubmit={ e => this.handleSubmit(e) }>
        <fieldset>
          <legend>General Information</legend>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">First Name</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="firstName" defaultValue={ user.firstName } />
            </div>
          </FormGroup>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">Last Name</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="lastName" defaultValue={ user.lastName } />
            </div>
          </FormGroup>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">Username</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="userName" defaultValue={ user.userName } />
            </div>
          </FormGroup>

          <UserEmailChanger email={ user.email } onClick={ hideResponseMessage }/>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">Date of Birth</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="bday" defaultValue={ new Date(user.dateOfBirth * 1000).toLocaleString("en-US", options) } />
            </div>
          </FormGroup>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">Mobile Phone</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="mobilePhone" defaultValue={ user.mobilePhone } />
            </div>
          </FormGroup>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">Country</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="country" defaultValue={ user.country } />
            </div>
          </FormGroup>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">State</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="state" defaultValue={ user.state } />
            </div>
          </FormGroup>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">City</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="city" defaultValue={ user.city } />
            </div>
          </FormGroup>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">Address Line</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="address" defaultValue={ user.addressLine } />
            </div>
          </FormGroup>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">Zipcode</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="zipcode" defaultValue={ user.zipcode } />
            </div>
          </FormGroup>

          <Button
            type="submit"
            className="btn-primary pull-right">
            Save Changes
          </Button>

        </fieldset>
      </form>
    )
  }
}
