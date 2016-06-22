import React, { Component } from 'react'
import { connect } from 'react-redux'

class ProfileViewer extends Component {
  render() {
    const { currentUser } = this.props.users

    return(
      <div className="row">
        <div className="form-horizontal col-sm-6">
        <filedset >
          <legend>General Information</legend>
          <div className="form-horizontal">

            <div className="form-group">
              <label className="col-sm-3 control-label">First Name</label>
              <div className="col-sm-9">
                <p className="form-control-static">{currentUser.firstName}</p>
              </div>
            </div>

            <div className="form-group">
              <label className="col-sm-3 control-label">Last Name</label>
              <div className="col-sm-9">
                <p className="form-control-static">{currentUser.lastName}</p>
              </div>
            </div>

            <div className="form-group">
              <label className="col-sm-3 control-label">User Name</label>
              <div className="col-sm-9">
                <p className="form-control-static">{currentUser.userName}</p>
              </div>
            </div>

            <div className="form-group">
              <label className="col-sm-3 control-label">Email</label>
              <div className="col-sm-9">
                <p className="form-control-static">{currentUser.email}</p>
              </div>
            </div>

            <div className="form-group">
              <label className="col-sm-3 control-label">Mobile Phone</label>
              <div className="col-sm-9">
                <p className="form-control-static">{currentUser.mobilePhone}</p>
              </div>
            </div>

            <div className="form-group">
              <label className="col-sm-3 control-label">Date of Birth</label>
              <div className="col-sm-9">
                <p className="form-control-static">{currentUser.dateOfBirth}</p>
              </div>
            </div>

            <div className="form-group">
              <label className="col-sm-3 control-label">Address</label>
              <div className="col-sm-9">
                <p className="form-control-static">{currentUser.country}, {currentUser.state ? currentUser.state : ""}, {currentUser.city}, {currentUser.addressLine}, {currentUser.zipcode}</p>
              </div>
            </div>
          </div>
        </filedset>
        </div>
      </div>
    )
  }
}

function mapStateToProps(state) {
  return state
}

export default connect(mapStateToProps)(ProfileViewer)
