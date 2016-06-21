import React, { Component } from 'react'

export default class Profile extends Component {
  render() {
    return(
      <form >
        <div className="form-group">
          <label>Username: </label>
          <input type="text" className="form-control" name="userName" defaultValue="J.Doe" />
        </div>
        <div className="form-group">
          <label>First Name: </label>
          <input type="text" className="form-control" name="firstName" defaultValue="Jane" />
        </div>
        <div className="form-group">
          <label>Last Name: </label>
          <input type="text" className="form-control" name="lastName" defaultValue="Doe" />
        </div>
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
        <button className="btn btn-primary" type="submit">Send</button>
      </form>
    );
  }
}
