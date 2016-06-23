import * as types from '../constants/actionTypes'

function showSuccessResponseMessage(message) {
  return {
    type: types.SHOW_SUCCESS_RESPONSE_MESSAGE,
    message
  }
}

function showFailureResponseMessage(message) {
  return {
    type: types.SHOW_FAILURE_RESPONSE_MESSAGE,
    message
  }
}

export function showResponseMessage(message, type) {
  return (dispatch) => {
    if(type == 'success')
      dispatch(showSuccessResponseMessage(message))
    else dispatch(showFailureResponseMessage(message))
  }
}

export function hideResponseMessage() {
  return {
    type: types.HIDE_RESPONSE_MESSAGE
  }
}
