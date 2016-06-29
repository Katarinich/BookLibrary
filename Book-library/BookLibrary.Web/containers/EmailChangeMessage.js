import React, { Component } from 'react'
import { connect } from 'react-redux'

import { continueEmailChange, hideResponseMessage, logoutUser } from '../actions'
import HttpResponseMessage from '../components/HttpResponseMessage'

class EmailChangeMessage extends Component {
  componentWillMount() {
    const { currentUser } = this.props.users

    if(currentUser) this.props.logoutUser()
  }

  componentDidMount() {
    this.props.continueEmailChange(this.props.params.codeValue)
  }

  componentWillUnmount() {
    this.props.hideResponseMessage()
  }

  render() {
    const { type, message } = this.props.response

    if(type) {
      return(
        <HttpResponseMessage type={ type } message={ message } />
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

export default connect(mapStateToProps, { continueEmailChange, hideResponseMessage, logoutUser })(EmailChangeMessage)
