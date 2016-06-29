import React, { Component } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router'

import { confirmEmail, hideResponseMessage } from '../actions'
import HttpResponseMessage from '../components/HttpResponseMessage'

class EmailConfirmMessage extends Component {
  componentDidMount() {
    this.props.confirmEmail(this.props.params.codeValue)
  }

  componentWillUnmount() {
    this.props.hideResponseMessage()
  }

  render() {
    const { type, message } = this.props.response
    const { currentUser } = this.props.users

    if(type) {
      return(
        <div style={{textAlign: 'center'}}>
          <HttpResponseMessage type={ type } message={ message } />
          {currentUser ? <Link to="/">Go to main page</Link> : <Link to="/login">Sing In</Link> }
        </div>
      )
    }
    else {
      return(<div></div>)
    }
  }
}

function mapStateToProps(state) {
  return state
}

export default connect(mapStateToProps, { confirmEmail, hideResponseMessage })(EmailConfirmMessage)
