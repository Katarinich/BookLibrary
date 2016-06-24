import React, { Component } from 'react'
import { connect } from 'react-redux'

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

export default connect(mapStateToProps, { confirmEmail, hideResponseMessage })(EmailConfirmMessage)
