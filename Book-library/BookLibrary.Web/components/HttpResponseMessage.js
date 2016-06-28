import React, { Component } from 'react'
import ReactDOM from 'react-dom'

export default class HttpResponseMessage extends Component {
  render() {
    const { type, message, onClick } = this.props
    const cssClassName = "alert alert-dismissible alert-" + type
    const messageTitle = type == 'success' ? 'Success!' : 'Failure!'

    return(
      <div className={ cssClassName } role="alert">
        <strong>{ messageTitle }</strong> { message }
      </div>
    )
  }
}
