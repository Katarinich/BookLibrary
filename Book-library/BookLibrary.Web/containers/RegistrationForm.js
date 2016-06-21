import React, { Component } from 'react';
import { Button, FormGroup, FormControl, ControlLabel } from 'react-bootstrap';

export default class RegistrationForm extends Component {
  handleSubmit(e) {
    e.preventDefault()

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

    fetch('http://localhost:51407/users/signup', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(formData)
    })
  }

  render() {
    return(
      <form onSubmit={ e => this.handleSubmit(e) } >
        <div className="form-group">
          <label>Username: </label>
          <input type="text" className="form-control" name="userName" defaultValue="J.Doe" />
        </div>
        <FormGroup>
          <ControlLabel>First Name</ControlLabel>
          <FormControl type="text" name="firstName" defaultValue="Jane" />
        </FormGroup>
        <FormGroup>
          <ControlLabel>Last Name</ControlLabel>
          <FormControl type="text" name="lastName" defaultValue="Doe" />
        </FormGroup>
        <div className="form-group">
          <label>Email: </label>
          <input type="email" className="form-control" name="email" defaultValue="jane.doe@example.com" />
        </div>
        <div className="form-group">
          <label>Birthday: </label>
          <input type="date" className="form-control" name="bday" />
        </div>
        <div className="form-group">
          <label>Mobile Phone: </label>
          <input type="text" className="form-control" name="phone" defaultValue="+123456789" />
        </div>
        <div className="form-group">
          <label>Country: </label>
          <input type="text" className="form-control" name="country" defaultValue="Belarus" />
        </div>
        <div className="form-group">
          <label>State: </label>
          <input type="text" className="form-control" name="state" defaultValue="-" />
        </div>
        <div className="form-group">
          <label>City: </label>
          <input type="text" className="form-control" name="city" defaultValue="Grodno" />
        </div>
        <div className="form-group">
          <label>Address: </label>
          <input type="text" className="form-control" name="address" defaultValue="qwerty" />
        </div>
        <div className="form-group">
          <label>Zipcode: </label>
          <input type="text" className="form-control" name="zipcode" defaultValue="123456" />
        </div>
        <div className="form-group">
          <label>Password: </label>
          <input type="password" className="form-control" name="password" />
        </div>
        <Button type="submit">
          Submit
        </Button>
      </form>
    )
  }
}
