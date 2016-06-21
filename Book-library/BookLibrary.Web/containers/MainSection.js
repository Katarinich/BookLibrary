import React, { Component } from 'react'
import { Link } from 'react-router'
import { connect } from 'react-redux'

import Header from './Header'
import UserGrid from './UserGrid'
import { getUsers } from '../actions'

class MainSection extends Component {
  componentDidMount() {
    this.props.getUsers()
  }

  render() {
    const { children } = this.props
    const { users } = this.props.users
    return(
      <div>
        <Header />
        { children ? children : users ? <UserGrid users={ users } /> : ""}
      </div>
    )
  }
}

function mapStateToProps(state) {
  return state
}

export default connect(mapStateToProps, { getUsers })(MainSection)
