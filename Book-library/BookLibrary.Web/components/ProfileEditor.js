import React, { Component } from 'react'

import EditGeneralInformation from './EditGeneralInformation'
import EditSecurityInformation from './EditSecurityInformation'

export default class ProfileEditor extends Component {
  render() {
    const { user, updateGeneralUserInfo } = this.props

    return(
      <div className="row">
        <div className="form-horizontal col-sm-6">
          <EditGeneralInformation onSubmit={ updateGeneralUserInfo } user={ user } />
        </div>
        <div className="form-horizontal col-sm-6">
          <EditSecurityInformation user={ user } />
        </div>
      </div>
    )
  }
}
