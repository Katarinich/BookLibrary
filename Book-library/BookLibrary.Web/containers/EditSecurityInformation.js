import React, { Component } from 'react'
import { connect } from 'react-redux'
import { Button, FormGroup, FormControl, ControlLabel } from 'react-bootstrap'

export default class EditSecurityInformation extends Component {
  render() {
    const { user } = this.props

    return(
      <form>
        <fieldset>
          <legend>Security Information</legend>

          <div className="form-group">
            <label className="col-sm-3 control-label">Roles</label>
            <div className="col-sm-9">
              <p className="form-control-static">{user.roles}</p>
            </div>
          </div>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">Current Password</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="password" name="password" />
            </div>
          </FormGroup>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">New Password</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="password" name="newPassword" />
            </div>
          </FormGroup>

          <FormGroup>
            <ControlLabel bsClass="control-label col-sm-3">Confirm New Password</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="password" name="newPasswordConfirmed" />
            </div>
          </FormGroup>

          <Button
            type="submit"
            className="btn-primary pull-right">
            Change Password
          </Button>

        </fieldset>
      </form>
    )
  }
}
