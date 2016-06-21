import React, { Component } from 'react'

import { Table } from 'react-bootstrap'

export default class UserGrid extends Component {
  render() {
    const users = this.props.users.map(function(user) {
      return <tr>
               <td>{ user.id }</td>
               <td>{ user.userName }</td>
               <td>{ user.firstName }</td>
               <td>{ user.lastName }</td>
            </tr>
    })
    return(
      <Table striped bordered condensed hover>
        <thead>
          <tr>
            <th>Id</th>
            <th>UserName</th>
            <th>First Name</th>
            <th>Last Name</th>
          </tr>
        </thead>
        <tbody>
          { users }
        </tbody>
      </Table>
    )
  }
}
