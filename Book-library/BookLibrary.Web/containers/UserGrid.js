import React, { Component } from 'react'

import { Table } from 'react-bootstrap'

export default class UserGrid extends Component {
  render() {
    return(
      <Table striped bordered condensed hover>
        <thead>
          <tr>
            <th>Id</th>
            <th>User</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>1</td>
            <td></td>
            <td></td>
            <td></td>
          </tr>
          <tr>
            <td>2</td>
            <td></td>
            <td></td>
            <td></td>
          </tr>
          <tr>
            <td>3</td>
            <td></td>
            <td></td>
            <td></td>
          </tr>
          <tr>
            <td>4</td>
            <td></td>
            <td></td>
            <td></td>
          </tr>
          <tr>
            <td>5</td>
            <td></td>
            <td></td>
            <td></td>
          </tr>
        </tbody>
      </Table>
    )
  }
}
