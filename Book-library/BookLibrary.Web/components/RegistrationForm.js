import React, { Component, PropTypes } from 'react'
import { Button, FormGroup, FormControl, ControlLabel, HelpBlock } from 'react-bootstrap'
import revalidator from 'revalidator'
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'
import moment from 'moment'

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

    var schema = {
      "type": "object",
      "properties": {
        "userName": {"type": "string", "pattern": "^[a-zA-Z0-9_-]{3,16}$", "messages": {"pattern": "UserName can contain only letters and numbers."}},
        "firstName": {"type": "string", "pattern": "^.+$", "messages": {"pattern": "First name can't be empty."}},
        "lastName": {"type": "string", "pattern": "^.+$", "messages": {"pattern": "Last name can't be empty."}},
        "email": {"type": "string", "pattern": "^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$", "messages": {"pattern": "Email is not valid."}},
        "dateOfBirth": {"type": "integer", "maximum": new Date().getTime() / 1000, "messages": {"maximum": "Date of birth is not valid."}},
        "mobilePhone": {"type": "string", "pattern": "^(\\+|\\d)[0-9]{7,16}$", "messages": {"pattern": "Mobile phone is not valid."}},
        "country": {"type": "string", "pattern": "^[a-zA-Z ]{3,16}$", "messages": {"pattern": "Country can contain only letters."}},
        "state": {"type": "string"},
        "city": {"type": "string", "pattern": "^[a-zA-Z ]{3,16}$", "messages": {"pattern": "City can contain only letters."}},
        "addressLine": {"type": "string", "pattern": "^.+$"},
        "zipcode": {"type": "string", "pattern": "^.+$"},
        "password": {"type": "string", "pattern": "^[a-zA-Z0-9_-]{8,18}$", "messages": {"pattern": "Password can contain only letters and numbers and contains from 8 to 18 characters."}}
      }
    }

    var formData = {
      userName: $('[name=userName]').val(),
      firstName: $('[name=firstName]').val(),
      lastName: $('[name=lastName]').val(),
      email: $('[name=email]').val(),
      dateOfBirth: new Date($('[name=dateOfBirth]').val()).getTime() / 1000,
      mobilePhone: $('[name=mobilePhone]').val(),
      country: $('[name=country]').val(),
      state: $('[name=state]').val(),
      city: $('[name=city]').val(),
      addressLine: $('[name=addressLine]').val(),
      zipcode: $('[name=zipcode]').val(),
      password: $('[name=password]').val()
    }

    console.log(revalidator.validate(formData, schema))

    let validation = revalidator.validate(formData, schema)

    if(validation.errors.length == 0) {
      //onSubmit(formData)
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
          <Button type="submit">
            Sign Up
          </Button>
        </form>
      </div>
    )
  }
}
