import React, { Component } from 'react'
import { Link } from 'react-router'

export default class UserGrid extends Component {
  render() {
    const { user } = this.props
    const profileUrl = '/profile/' + user.id

    return(
      <tr>
        <td>{ user.id }</td>
        <td><Link to={ profileUrl }>{ user.userName }</Link></td>
        <td>{ user.firstName }</td>
        <td>{ user.lastName }</td>
      </tr>
    )
  }
}
