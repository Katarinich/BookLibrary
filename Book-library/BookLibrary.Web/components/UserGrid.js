import React, { Component } from 'react'
import { Table } from 'react-bootstrap'

import UserGridItem from './UserGridItem'

export default class UserGrid extends Component {
  render() {
    const users = this.props.users.map(function(user) {
      return <UserGridItem user={ user } key={ user.id } />
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
