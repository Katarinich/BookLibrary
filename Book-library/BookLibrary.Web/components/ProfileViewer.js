import React, { Component } from 'react'

export default class ProfileViewer extends Component {
  render() {
    const { user } = this.props
    const options = {
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    }

    return(
      <div className="row">
        <div className="form-horizontal col-sm-6">
        <filedset >
          <legend>General Information</legend>
          <div className="form-horizontal">

            <div className="form-group">
              <label className="col-sm-3 control-label">First Name</label>
              <div className="col-sm-9">
                <p className="form-control-static">{user.firstName}</p>
              </div>
            </div>

            <div className="form-group">
              <label className="col-sm-3 control-label">Last Name</label>
              <div className="col-sm-9">
                <p className="form-control-static">{user.lastName}</p>
              </div>
            </div>

            <div className="form-group">
              <label className="col-sm-3 control-label">User Name</label>
              <div className="col-sm-9">
                <p className="form-control-static">{user.userName}</p>
              </div>
            </div>

            <div className="form-group">
              <label className="col-sm-3 control-label">Email</label>
              <div className="col-sm-9">
                <p className="form-control-static">{user.email}</p>
              </div>
            </div>

            <div className="form-group">
              <label className="col-sm-3 control-label">Mobile Phone</label>
              <div className="col-sm-9">
                <p className="form-control-static">{user.mobilePhone}</p>
              </div>
            </div>

            <div className="form-group">
              <label className="col-sm-3 control-label">Date of Birth</label>
              <div className="col-sm-9">
                <p className="form-control-static">{new Date(user.dateOfBirth * 1000).toLocaleString("en-US", options)}</p>
              </div>
            </div>

            <div className="form-group">
              <label className="col-sm-3 control-label">Address</label>
              <div className="col-sm-9">
                <p className="form-control-static">{user.country}, {user.state ? user.state : ""}, {user.city}, {user.addressLine}, {user.zipcode}</p>
              </div>
            </div>
          </div>
        </filedset>
        </div>
      </div>
    )
  }
}
