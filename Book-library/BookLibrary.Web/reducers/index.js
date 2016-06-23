import { combineReducers } from 'redux'

import users from './users'
import response from './response'

export default combineReducers({
  users,
  response
})
