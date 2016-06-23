import React, { Component } from 'react'
import { Link } from 'react-router'
import { connect } from 'react-redux'

import Header from '../components/Header'
import UserGrid from '../components/UserGrid'
import { getUsers } from '../actions'

class MainSection extends Component {
  componentDidMount() {
    this.props.getUsers()
  }

  render() {
    const { children } = this.props
    const { users, currentUser } = this.props.users

    if(users) {
      return(
        <div>
          <Header user={ currentUser } />
          { children ? children : <UserGrid users={ users } /> }
        </div>
      )
    }
    return (<div></div>)
  }
}

function mapStateToProps(state) {
  return state
}

export default connect(mapStateToProps, { getUsers })(MainSection)
