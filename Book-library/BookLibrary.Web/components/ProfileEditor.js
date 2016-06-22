import React, { Component } from 'react'

import EditGeneralInformation from '../containers/EditGeneralInformation'
import EditSecurityInformation from '../containers/EditSecurityInformation'

export default class ProfileEditor extends Component {
  render() {
    const { user } = this.props

    return(
      <div className="row">
        <div className="form-horizontal col-sm-6">
          <EditGeneralInformation user={ user } />
        </div>
        <div className="form-horizontal col-sm-6">
          <EditSecurityInformation user={ user } />
        </div>
      </div>
    )
  }
}
