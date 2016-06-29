import * as types from '../constants/actionTypes'

export default function user(state = { isFetching: false }, action) {
  switch(action.type) {
    case types.LOGIN_USER_REQUEST:
    case types.REGISTER_USER_REQUEST:
    case types.GET_USERS_REQUEST:
    case types.UPDATE_USER_REQUEST:
    case types.CONFIRM_EMAIL_REQUEST:
    case types.INITIATE_USER_EMAIL_CHANGE_REQUEST:
    case types.CONTINUE_EMAIL_CHANGE_REQUEST:
    case types.FINISH_EMAIL_CHANGE_REQUEST:
    case types.INITIATE_PASSWORD_RECOVERY_REQUEST:
    case types.FINISH_PASSWORD_RECOVERY_REQUEST:
    case types.PASSWORD_CHANGE_REQUEST:
      return {
        ...state,
        isFetching: true
      }
    case types.INITIATE_USER_EMAIL_CHANGE_SUCCESS:
      return {
        ...state,
        currentUser: {...state.currentUser, pendingEmail: action.pendingEmail},
        isFetching: false,
        token: action.token
      }
    case types.LOGIN_USER_SUCCESS:
    case types.UPDATE_USER_SUCCESS:
      return {
        ...state,
        currentUser: action.user,
        token: action.token,
        isFetching: false
      }
    case types.FINISH_EMAIL_CHANGE_SUCCESS:
      return {
        ...state,
        currentUser: {...state.currentUser, pendingEmail: null, email: action.newEmailValue},
        token: action.token,
        isFetching: false
      }
    case types.LOGOUT_USER:
     return {
       isFetching: false
     }
    case types.PASSWORD_CHANGE_SUCCESS:
      return {
        ...state,
        isFetching: false,
        token: action.token
      }
    case types.REGISTER_USER_SUCCESS:
    case types.CONFIRM_EMAIL_SUCCESS:
    case types.CONTINUE_EMAIL_CHANGE_SUCCESS:
    case types.INITIATE_PASSWORD_RECOVERY_SUCCESS:
    case types.FINISH_PASSWORD_RECOVERY_SUCCESS:
      return {
        ...state,
        isFetching: false
      }
    case types.GET_USERS_SUCCESS:
      return {
        ...state,
        isFetching: false,
        users: action.users,
        token: action.token
      }
    case types.UPDATE_USER_FAILURE:
    case types.INITIATE_USER_EMAIL_CHANGE_FAILURE:
    case types.PASSWORD_CHANGE_FAILURE:
      return {
        ...state,
        isFetching: false,
        error: action.err,
        token: action.token
      }
    case types.GET_USERS_FAILURE:
    case types.LOGIN_USER_FAILURE:
    case types.REGISTER_USER_FAILURE:
    case types.CONFIRM_EMAIL_FAILURE:
    case types.CONTINUE_EMAIL_CHANGE_FAILURE:
    case types.FINISH_EMAIL_CHANGE_FAILURE:
    case types.INITIATE_PASSWORD_RECOVERY_FAILURE:
    case types.FINISH_PASSWORD_RECOVERY_FAILURE:
      return {
        ...state,
        isFetching: false,
        error: action.err
      }
    default:
      return state;
  }
}
