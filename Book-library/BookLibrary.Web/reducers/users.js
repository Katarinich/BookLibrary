import * as types from '../constants/actionTypes'

export default function user(state = { isFetching: false }, action) {
  switch(action.type) {
    case types.LOGIN_USER_REQUEST:
    case types.LOGOUT_USER_REQUEST:
    case types.REGISTER_USER_REQUEST:
    case types.GET_USERS_REQUEST:
    case types.UPDATE_USER_REQUEST:
      return {
        ...state,
        isFetching: true
      }
    case types.LOGIN_USER_SUCCESS:
    case types.UPDATE_USER_SUCCESS:
      return {
        ...state,
        currentUser: action.user,
        isFetching: false
      }
    case types.LOGOUT_USER_SUCCESS:
     return {
       isFetching: false
     }
    case types.REGISTER_USER_SUCCESS:
      return {
        ...state,
        isFetching: false
      }
    case types.GET_USERS_SUCCESS:
      return {
        ...state,
        isFetching: false,
        users: JSON.parse(action.users)
      }
    case types.GET_USERS_FAILURE:
    case types.LOGIN_USER_FAILURE:
    case types.LOGOUT_USER_FAILUR:
    case types.REGISTER_USER_FAILURE:
    case types.UPDATE_USER_FAILURE:
      return {
        ...state,
        isFetching: false,
        error: action.err
      }
    default:
      return state;
  }
}
