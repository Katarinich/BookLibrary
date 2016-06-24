import React, { Component } from 'react'
import { connect } from 'react-redux'

import ProfileViewer from '../components/ProfileViewer'
import ProfileEditor from '../components/ProfileEditor'
import { updateUser, initiateUserEmailChange } from '../actions'

class Profile extends Component {
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

export default connect(mapStateToProps, { updateUser, initiateUserEmailChange })(Profile)
