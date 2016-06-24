import React, { Component } from 'react'
import { Button, FormGroup, FormControl, ControlLabel, ButtonGroup } from 'react-bootstrap'

export default class UserEmailChanger extends Component {
  constructor(props) {
    super(props)

    this.state = {
      editing: false
    }
  }

  handleChangeEmailBtnClick() {
    this.setState({ editing: true })
  }

  handleCancelChangeEmailBtnClick() {
    this.setState({ editing: false })
  }

  handleChangeEmail() {
    var newEmailValue = $('[name=email]').val()
    this.props.onClick(newEmailValue)
  }

  renderEmailSection() {
    const { email } = this.props
    const { editing } = this.state

    if(editing)
      return(
        <div>
          <div className="col-sm-5">
            <FormControl type="email" name="email" defaultValue={ email } />
          </div>

          <div className="col-sm-4">
            <ButtonGroup className="pull-right">
              <Button className="btn-primary" onClick={ this.handleChangeEmail.bind(this) }>
                Change
              </Button>
              <Button onClick={ this.handleCancelChangeEmailBtnClick.bind(this) }>
                Cancel
              </Button>
            </ButtonGroup>
          </div>
        </div>
      )

    return (
      <div>
        <div className="col-sm-5">
          <p className="form-control-static">{email}</p>
        </div>

        <div className="col-sm-4">
          <Button
            className="pull-right"
            onClick={this.handleChangeEmailBtnClick.bind(this)}>
            Change Email
          </Button>
        </div>
      </div>
    )
  }

  render() {
    return(
      <div className="form-group">
        <label className="col-sm-3 control-label">Email</label>
        { this.renderEmailSection() }
      </div>
    )
  }
}
