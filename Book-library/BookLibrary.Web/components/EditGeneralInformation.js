import React, { Component, PropTypes } from 'react'
import { connect } from 'react-redux'
import { Button, FormGroup, FormControl, ControlLabel, HelpBlock } from 'react-bootstrap'
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'
import moment from 'moment'

import UserEmailChanger from './UserEmailChanger'
import { validateUserDraft } from '../utils/validation'

export default class EditGeneralInformation extends Component {
  constructor(props) {
    super(props)

    this.state = {
      errors: []
    }
  }

  handleSubmit(e) {
    e.preventDefault()

    const { onSubmit, user } = this.props

    var formData = {
      email: user.email,
      userName: $('[name=userName]').val().trim(),
      firstName: $('[name=firstName]').val().trim(),
      lastName: $('[name=lastName]').val().trim(),
      dateOfBirth: new Date($('[name=dateOfBirth]').val()).getTime() / 1000,
      mobilePhone: $('[name=mobilePhone]').val().trim(),
      country: $('[name=country]').val().trim(),
      state: $('[name=state]').val().trim(),
      city: $('[name=city]').val().trim(),
      addressLine: $('[name=addressLine]').val().trim(),
      zipcode: $('[name=zipcode]').val().trim(),
      password: "12345678"
    }

    let validation = validateUserDraft(formData)

    if(validation.errors.length == 0) {
      onSubmit(formData)
    }

    this.setState({errors: validation.errors})
  }

  componentDidMount() {
    document.getElementsByClassName('react-datepicker__input-container')[0].style = 'display: block'
  }

  handleChange(date) {
    this.setState({dateOfBirth: moment(date)})
  }

  getValidationState(property) {
    const { errors } = this.state

    var result = errors.find(x => x.property == property)
    return result ? 'error' : 'success'
  }

  getValidationMessage(property) {
    const { errors } = this.state

    var result = errors.find(x => x.property == property)
    return result ? result.message : null
  }

  render() {
    const { user, initiateUserEmailChange } = this.props

    return(
      <form onSubmit={ e => this.handleSubmit(e) }>
        <fieldset>
          <legend>General Information</legend>

          <FormGroup validationState={ this.getValidationState('firstName') }>
            <ControlLabel bsClass="control-label col-sm-3">First Name</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="firstName" defaultValue={ user.firstName } />
              { this.getValidationMessage('firstName') && <HelpBlock>{this.getValidationMessage('firstName')}</HelpBlock> }
            </div>
          </FormGroup>

          <FormGroup validationState={ this.getValidationState('lastName') }>
            <ControlLabel bsClass="control-label col-sm-3">Last Name</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="lastName" defaultValue={ user.lastName }/>
              { this.getValidationMessage('lastName') && <HelpBlock>{this.getValidationMessage('lastName')}</HelpBlock> }
            </div>
          </FormGroup>

          <FormGroup validationState={ this.getValidationState('userName') }>
            <ControlLabel bsClass="control-label col-sm-3">Username</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="userName" defaultValue={ user.userName } />
              { this.getValidationMessage('userName') && <HelpBlock>{ this.getValidationMessage('userName') }</HelpBlock> }
            </div>
          </FormGroup>

          <UserEmailChanger email={ user.email } pendingEmail={ user.pendingEmail } onClick={ initiateUserEmailChange }/>

          <FormGroup validationState={ this.getValidationState('dateOfBirth') }>
            <ControlLabel bsClass="control-label col-sm-3">Date of Birth</ControlLabel>
            <div className="col-sm-9">
              <DatePicker
                dateFormat={moment.localeData().longDateFormat('L')}
                className="form-control"
                selected={moment.unix(user.dateOfBirth).isValid() ? moment.unix(user.dateOfBirth) : null}
                onChange={this.handleChange.bind(this)}
                name="dateOfBirth"
                maxDate={moment()}
                minDate={moment().subtract(100, 'years')}
                showYearDropdown
                placeholderText='Date of Birth'
              />
              { this.getValidationMessage('dateOfBirth') && <HelpBlock>{this.getValidationMessage('dateOfBirth')}</HelpBlock> }
            </div>
          </FormGroup>

          <FormGroup validationState={ this.getValidationState('mobilePhone') }>
            <ControlLabel bsClass="control-label col-sm-3">Mobile Phone</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="mobilePhone" defaultValue={ user.mobilePhone } />
              { this.getValidationMessage('mobilePhone') && <HelpBlock>{this.getValidationMessage('mobilePhone')}</HelpBlock> }
            </div>
          </FormGroup>

          <FormGroup validationState={ this.getValidationState('country') }>
            <ControlLabel bsClass="control-label col-sm-3">Country</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="country" defaultValue={ user.country } />
              { this.getValidationMessage('country') && <HelpBlock>{this.getValidationMessage('country')}</HelpBlock> }
            </div>
          </FormGroup>

          <FormGroup validationState={ this.getValidationState('state') }>
            <ControlLabel bsClass="control-label col-sm-3">State</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="state" defaultValue={ user.state } />
            </div>
          </FormGroup>

          <FormGroup validationState={ this.getValidationState('city') }>
            <ControlLabel bsClass="control-label col-sm-3">City</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="city" defaultValue={ user.city } />
              { this.getValidationMessage('city') && <HelpBlock>{this.getValidationMessage('city')}</HelpBlock> }
            </div>
          </FormGroup>

          <FormGroup validationState={ this.getValidationState('addressLine') }>
            <ControlLabel bsClass="control-label col-sm-3">Address Line</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="text" name="addressLine" defaultValue={ user.addressLine } />
              { this.getValidationMessage('addressLine') && <HelpBlock>{this.getValidationMessage('addressLine')}</HelpBlock> }
            </div>
          </FormGroup>

          <FormGroup validationState={ this.getValidationState('zipcode') }>
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
