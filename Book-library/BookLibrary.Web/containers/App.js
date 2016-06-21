import React, { Component } from 'react'
import { connect } from 'react-redux'

import LoginSection from './LoginSection'
import MainSection from './MainSection'

export default class App extends Component {
  render() {
    return (
      <div className="container">
        { this.props.children }
      </div>
    )
  }
}
