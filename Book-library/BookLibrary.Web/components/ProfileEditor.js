import React, { Component } from 'react'

import EditGeneralInformation from './EditGeneralInformation'
import EditSecurityInformation from './EditSecurityInformation'
import HttpResponseMessage from './HttpResponseMessage'

export default class ProfileEditor extends Component {
  render() {
    const { user, updateGeneralUserInfo, initiateUserEmailChange, type, message, passwordChange } = this.props

    return(
      <div className="row">
        { type && <HttpResponseMessage type={ type } message={ message } /> }

        <div className="form-horizontal col-sm-6">
          <EditGeneralInformation onSubmit={ updateGeneralUserInfo } user={ user } initiateUserEmailChange={ initiateUserEmailChange }/>
        </div>
        <div className="form-horizontal col-sm-6">
          <EditSecurityInformation user={ user } onSubmit={ passwordChange }/>
        </div>
      </div>
    )
  }
}
