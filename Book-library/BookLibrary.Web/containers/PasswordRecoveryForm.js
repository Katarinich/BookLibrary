import React, { Component } from 'react'
import { connect } from 'react-redux'
import { FormGroup, FormControl, Button } from 'react-bootstrap'
import { Link } from 'react-router'

import { finishPasswordRecovery, hideResponseMessage } from '../actions'
import HttpResponseMessage from '../components/HttpResponseMessage'

class PasswordRecoveryForm extends Component {
  componentWillUnmount() {
    if(this.props.response.type) this.props.hideResponseMessage()
  }

  handleSubmit(e) {
    e.preventDefault()

    var codeValue = this.props.params.codeValue
    var newPasswordValue = $('[name=password]').val()

    this.props.finishPasswordRecovery(codeValue, newPasswordValue)
  }

  render() {
    const { type, message } = this.props.response

    return(
      <div className="form-horizontal col-sm-6 col-sm-offset-3 text-center">
        { type && <HttpResponseMessage type={ type } message={ message } /> }

        <form onSubmit={ e => this.handleSubmit(e) }>
          <h3>Update Password</h3>
          <FormGroup bsClass="form-group form-group-lg">
            <div className="col-sm-12">
              <FormControl type="password" name="password" placeholder="Password" />
            </div>
          </FormGroup>
          <FormGroup bsClass="form-group form-group-lg">
            <div className="col-sm-12">
              <FormControl type="password" name="passwordConfirm" placeholder="Confirm Password" />
            </div>
          </FormGroup>
          <FormGroup>
            <div className="col-sm-6" style={{textAlign: 'left'}}>
              <Link to="/login" className="col-sm-6">Sign In</Link>
            </div>
          </FormGroup>
          <Button
            type="submit"
            bsSize="large"
            className="btn-primary">
            Update Password
          </Button>
        </form>
      </div>
    )
  }
}

function mapStateToProps(state) {
  return state
}

export default connect(mapStateToProps, { finishPasswordRecovery, hideResponseMessage })(PasswordRecoveryForm)
