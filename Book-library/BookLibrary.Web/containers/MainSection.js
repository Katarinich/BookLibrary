import React, { Component } from 'react'
import { Link } from 'react-router'
import { connect } from 'react-redux'

import Header from '../components/Header'
import UserGrid from '../components/UserGrid'
import { getUsers } from '../actions'

class MainSection extends Component {

  render() {
    const { children } = this.props
    const { users, currentUser } = this.props.users

      return(
        <div>
          <Header user={ currentUser } />
          { children ? children : <UserGrid users={ users } onMount={this.props.getUsers}/> }
        </div>
      )
  }
}

function mapStateToProps(state) {
  return state
}

export default connect(mapStateToProps, { getUsers })(MainSection)
