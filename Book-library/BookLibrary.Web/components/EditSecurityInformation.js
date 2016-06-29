import React, { Component } from 'react'
import { Button, FormGroup, FormControl, ControlLabel, HelpBlock } from 'react-bootstrap'

export default class EditSecurityInformation extends Component {
  constructor(props) {
    super(props)

    this.state = {
      errors: []
    }
  }

  handleSubmit(e) {
    e.preventDefault()

    var oldPasswordValue = $('[name=password]').val()
    var newPasswordValue = $('[name=newPassword]').val()
    var newPasswordValueConfirmed = $('[name=newPasswordConfirmed]').val()

    if(newPasswordValue == newPasswordValueConfirmed) {
      this.props.onSubmit(oldPasswordValue.trim(), newPasswordValue.trim())
      this.setState({ errors: [] })
    }
    else {
      this.setState({
        errors: [
          { property: 'newPasswordConfirmed', message: 'Confirmed password should be match with new password.' }
        ]
      })
    }
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
    const { user } = this.props

    return(
      <form onSubmit={ this.handleSubmit.bind(this) }>
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

          <FormGroup validationState={ this.getValidationState('newPasswordConfirmed') }>
            <ControlLabel bsClass="control-label col-sm-3">New Password</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="password" name="newPassword" />
            </div>
          </FormGroup>

          <FormGroup validationState={ this.getValidationState('newPasswordConfirmed') }>
            <ControlLabel bsClass="control-label col-sm-3">Confirm New Password</ControlLabel>
            <div className="col-sm-9">
              <FormControl type="password" name="newPasswordConfirmed" />
              { this.getValidationMessage('newPasswordConfirmed') && <HelpBlock>{this.getValidationMessage('newPasswordConfirmed')}</HelpBlock> }
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
