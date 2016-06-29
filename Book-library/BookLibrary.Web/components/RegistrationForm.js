import React, { Component, PropTypes } from 'react'
import { Button, FormGroup, FormControl, ControlLabel, HelpBlock } from 'react-bootstrap'
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'
import moment from 'moment'

import { validateUserDraft } from '../utils/validation'
import HttpResponseMessage from './HttpResponseMessage'

export default class RegistrationForm extends Component {
  constructor(props) {
    super(props)

    this.state = {
      errors: []
    }
  }

  handleSubmit(e) {
    e.preventDefault()

    const { onSubmit } = this.props

    var passwordConfirmedValue = $('[name=passwordConfirmed]').val()

    var formData = {
      userName: $('[name=userName]').val().trim(),
      firstName: $('[name=firstName]').val().trim(),
      lastName: $('[name=lastName]').val().trim(),
      email: $('[name=email]').val().trim(),
      dateOfBirth: new Date($('[name=dateOfBirth]').val()).getTime() / 1000,
      mobilePhone: $('[name=mobilePhone]').val().trim(),
      country: $('[name=country]').val().trim(),
      state: $('[name=state]').val().trim(),
      city: $('[name=city]').val().trim(),
      addressLine: $('[name=addressLine]').val().trim(),
      zipcode: $('[name=zipcode]').val().trim(),
      password: $('[name=password]').val().trim()
    }

    let validation = validateUserDraft(formData)

    if(validation.errors.length == 0) {
      if(passwordConfirmedValue == formData.password)
        onSubmit(formData)
      else validation.errors.push({ property: 'passwordConfirmed', message: 'Passwords should match.' })
    }

    this.setState({errors: validation.errors})
  }

  componentWillUnmount() {
    if(this.props.type) this.props.onUnmount()
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
    const { type, message } = this.props
    const { dateOfBirth } = this.state

    return(
      <div>
        { type && <HttpResponseMessage type={ type } message={ message } /> }

        <form onSubmit={ e => this.handleSubmit(e) } >
          <FormGroup validationState={ this.getValidationState('userName') }>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="userName" defaultValue="JaneDoe" />
              <HelpBlock>UserName can contain only letters and numbers.</HelpBlock>
            </div>
          </FormGroup>
          <FormGroup validationState={ this.getValidationState('firstName') }>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="firstName" defaultValue="Jane" />
              { this.getValidationMessage('firstName') && <HelpBlock>{this.getValidationMessage('firstName')}</HelpBlock> }
            </div>
          </FormGroup>
          <FormGroup validationState={ this.getValidationState('lastName') }>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="lastName" defaultValue="Doe" />
              { this.getValidationMessage('lastName') && <HelpBlock>{this.getValidationMessage('lastName')}</HelpBlock> }
            </div>
          </FormGroup>
          <FormGroup validationState={ this.getValidationState('email') }>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="email" name="email" defaultValue="jane.doe@example.com" />
              { this.getValidationMessage('email') && <HelpBlock>{this.getValidationMessage('email')}</HelpBlock> }
            </div>
          </FormGroup>
          <FormGroup validationState={ this.getValidationState('dateOfBirth') }>
            <div className="col-sm-10 col-sm-offset-1">
              <DatePicker
                dateFormat={moment.localeData().longDateFormat('L')}
                className="form-control"
                selected={moment(dateOfBirth).isValid() ? moment(dateOfBirth) : null}
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
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="mobilePhone" defaultValue="123456789" />
              { this.getValidationMessage('mobilePhone') && <HelpBlock>{this.getValidationMessage('mobilePhone')}</HelpBlock> }
            </div>
          </FormGroup>
          <FormGroup validationState={ this.getValidationState('country') }>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="country" defaultValue="Belarus" />
              { this.getValidationMessage('country') && <HelpBlock>{this.getValidationMessage('country')}</HelpBlock> }
            </div>
          </FormGroup>
          <FormGroup validationState={ this.getValidationState('state') }>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="state" defaultValue="None" />
            </div>
          </FormGroup>
          <FormGroup validationState={ this.getValidationState('city') }>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="city" defaultValue="Grodno" />
              { this.getValidationMessage('city') && <HelpBlock>{this.getValidationMessage('city')}</HelpBlock> }
            </div>
          </FormGroup>
          <FormGroup validationState={ this.getValidationState('addressLine') }>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="addressLine" defaultValue="qwerty" />
              { this.getValidationMessage('addressLine') && <HelpBlock>{this.getValidationMessage('addressLine')}</HelpBlock> }
            </div>
          </FormGroup>
          <FormGroup validationState={ this.getValidationState('zipcode') }>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="text" name="zipcode" defaultValue="123456" />
            </div>
          </FormGroup>
          <FormGroup validationState={ this.getValidationState('password') }>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="password" name="password" defaultValue="12345678" />
              <HelpBlock>Password can contain only letters and numbers and contains from 8 to 18 characters.</HelpBlock>
            </div>
          </FormGroup>
          <FormGroup validationState={ this.getValidationState('passwordConfirmed') }>
            <div className="col-sm-10 col-sm-offset-1">
              <FormControl type="password" name="passwordConfirmed" defaultValue="12345678" />
              { this.getValidationMessage('passwordConfirmed') && <HelpBlock>{this.getValidationMessage('passwordConfirmed')}</HelpBlock> }
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
