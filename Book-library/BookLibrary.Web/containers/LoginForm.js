import React, { Component } from 'react';
import { Button, FormGroup, FormControl, ControlLabel } from 'react-bootstrap';

export default class LoginForm extends Component {
  handleSubmit(e) {
    e.preventDefault()

    var formData = {
      login: $('[name=userName]').val(),
      password: $('[name=password]').val()
    }

    fetch('http://localhost:51407/users/signin', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(formData)
    })
    .then(response => response.json())
    .then(json => console.log(json))
  }

  render() {
    return(
      <form onSubmit={ e => this.handleSubmit(e) } >
        <FormGroup>
          <ControlLabel>Username: </ControlLabel>
          <FormControl type="text" name="userName" defaultValue="J.Doe" />
        </FormGroup>
        <FormGroup>
          <ControlLabel>Password: </ControlLabel>
          <FormControl type="password" name="password" />
        </FormGroup>
        <Button type="submit">
          Submit
        </Button>
      </form>
    )
  }
}
