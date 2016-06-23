import React, { Component } from 'react'

export default class HttpResponseMessage extends Component {
  render() {
    const { type, message } = this.props
    const cssClassName = "alert alert-dismissible alert-" + type
    const messageTitle = type == 'success' ? 'Success!' : 'Failure!'

    return(
      <div className={ cssClassName } role="alert">
        <button type="button" className="close" data-dismiss="alert" aria-label="Close" >
          <span aria-hidden="true">&times;</span>
        </button>
        <strong>{ messageTitle }</strong> { message }
      </div>
    )
  }
}
