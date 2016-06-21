import React, { Component } from 'react'
import { connect } from 'react-redux'

import LoginSection from './LoginSection'
import MainSection from './MainSection'

export default class App extends Component {
  render() {
    const { currentUser } = this.props.users
    return (
      <div className="container">
        { this.props.children }
      </div>
    )
  }
}

function mapStateToProps(state) {
  return state
}

export default connect(mapStateToProps)(App)
