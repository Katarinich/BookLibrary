import React, { Component } from 'react'
import { connect } from 'react-redux'
import moment from 'moment'

import ProfileViewer from '../components/ProfileViewer'
import ProfileEditor from '../components/ProfileEditor'
import { updateUser, initiateUserEmailChange, passwordChange, hideResponseMessage, logoutUser } from '../actions'
import getDecodedTokenData from '../actions/utils/decode-token'

class Profile extends Component {
  componentWillMount() {
    const { token } = this.props.users

    if(moment.unix(getDecodedTokenData(token).expiryDate).isBefore(moment())) {
      this.props.logoutUser()
    }
  }

  render() {
    const { currentUser, users } = this.props.users
    const { type, message } = this.props.response
    const { userId } = this.props.params
    var profileUser = users.find(u => u.id == userId)

    return(
      <div>
        { currentUser.id == userId ? <ProfileEditor
                                        updateGeneralUserInfo={ this.props.updateUser }
                                        initiateUserEmailChange={ this.props.initiateUserEmailChange }
                                        passwordChange={ this.props.passwordChange }
                                        onUnmount={ this.props.hideResponseMessage }
                                        user={ currentUser }
                                        type={ type }
                                        message={ message } />
                                   : <ProfileViewer user={ profileUser } />}
      </div>
    )
  }
}

function mapStateToProps(state) {
  return state
}

export default connect(mapStateToProps, { updateUser, initiateUserEmailChange, passwordChange, hideResponseMessage, logoutUser })(Profile)
