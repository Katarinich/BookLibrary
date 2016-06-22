import React, { Component } from 'react'
import { connect } from 'react-redux'

import ProfileViewer from '../components/ProfileViewer'
import ProfileEditor from '../components/ProfileEditor'

class Profile extends Component {
  render() {
    const { currentUser, users } = this.props.users
    const { userId } = this.props.params
    var profileUser = users.find(u => u.id == userId)

    return(
      <div>
        { currentUser.id == userId ? <ProfileEditor user={ currentUser } /> : <ProfileViewer user={ profileUser } />}
      </div>
    )
  }
}

function mapStateToProps(state) {
  return state
}

export default connect(mapStateToProps)(Profile)
