import * as types from '../constants/actionTypes'

export default function response(state = {}, action) {
  switch(action.type) {
    case types.SHOW_SUCCESS_RESPONSE_MESSAGE:
      return {
        type: 'success',
        message: action.message
      }
    case types.SHOW_FAILURE_RESPONSE_MESSAGE:
      return {
        type: 'danger',
        message: action.message
      }
    case types.HIDE_RESPONSE_MESSAGE:
      return {}
    default:
      return state
  }
}
