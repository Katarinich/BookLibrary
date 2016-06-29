import { browserHistory } from 'react-router'
import moment from 'moment'

import request from './utils/request'
import getDecodedTokenData from './utils/decode-token'
import * as types from '../constants/actionTypes'
import { showResponseMessage } from './response'

const apiUrl = 'http://localhost:51407/users'

function loginUserRequest(user) {
  return {
    type: types.LOGIN_USER_REQUEST,
    user
  }
}

function loginUserSuccess(user) {
  return {
    type: types.LOGIN_USER_SUCCESS,
    user: user.user,
    token: user.token
  }
}

function loginUserFailure(err) {
  return {
    type: types.LOGIN_USER_FAILURE,
    err
  }
}

function registerUserRequest(user) {
  return {
    type: types.REGISTER_USER_REQUEST,
    user
  }
}

function registerUserSuccess() {
  return {
    type: types.REGISTER_USER_SUCCESS
  }
}

function registerUserFailure(err) {
  return {
    type: types.REGISTER_USER_FAILURE,
    err
  }
}

function logoutUserRequest(user) {
  return {
    type: types.LOGOUT_USER_REQUEST,
    user
  }
}

function logoutUserSuccess() {
  return {
    type: types.LOGOUT_USER_SUCCESS
  }
}

function logoutUserFailure(err) {
  return {
    type: types.LOGOUT_USER_FAILURE,
    err
  }
}

function getUsersRequest() {
  return {
    type: types.GET_USERS_REQUEST
  }
}

function getUsersSuccess(users) {
  return {
    type: types.GET_USERS_SUCCESS,
    users: users.users,
    token: users.token
  }
}

function getUsersFailure(err) {
  return {
    type: types.GET_USERS_FAILURE,
    err
  }
}

function updateUserRequest(user) {
  return {
    type: types.UPDATE_USER_REQUEST,
    user
  }
}

function updateUserSuccess(user) {
  return {
    type: types.UPDATE_USER_SUCCESS,
    user: user.user,
    token: user.token
  }
}

function updateUserFailure(err) {
  return {
    type: types.UPDATE_USER_FAILURE,
    err: err.message,
    token: err.token
  }
}

function confirmEmailRequest() {
  return {
    type: types.CONFIRM_EMAIL_REQUEST
  }
}

function confirmEmailSuccess() {
  return {
    type: types.CONFIRM_EMAIL_SUCCESS
  }
}

function confirmEmailFailure(err) {
  return {
    type: types.CONFIRM_EMAIL_FAILURE,
    err
  }
}

function initiateUserEmailChangeRequest() {
  return {
    type: types.INITIATE_USER_EMAIL_CHANGE_REQUEST
  }
}

function initiateUserEmailChangeSuccess(response) {
  return {
    type: types.INITIATE_USER_EMAIL_CHANGE_SUCCESS,
    pendingEmail: response.pendingEmail
  }
}

function initiateUserEmailChangeFailure(err) {
  return {
    type: types.INITIATE_USER_EMAIL_CHANGE_FAILURE,
    err
  }
}

function continueEmailChangeRequest() {
  return {
    type: types.CONTINUE_EMAIL_CHANGE_REQUEST
  }
}

function continueEmailChangeSuccess() {
  return {
    type: types.CONTINUE_EMAIL_CHANGE_SUCCESS
  }
}

function continueEmailChangeFailure(err) {
  return {
    type: types.CONTINUE_EMAIL_CHANGE_FAILURE,
    err
  }
}

function finishEmailChangeRequest() {
  return {
    type: types.FINISH_EMAIL_CHANGE_REQUEST
  }
}

function finishEmailChangeSuccess(newEmailValue) {
  return {
    type: types.FINISH_EMAIL_CHANGE_SUCCESS,
    newEmailValue
  }
}

function finishEmailChangeFailure(err) {
  return {
    type: types.FINISH_EMAIL_CHANGE_FAILURE,
    err
  }
}

function initiatePasswordRecoveryRequest() {
  return {
    type: types.INITIATE_PASSWORD_RECOVERY_REQUEST
  }
}

function initiatePasswordRecoverySuccess() {
  return {
    type: types.INITIATE_PASSWORD_RECOVERY_SUCCESS
  }
}

function initiatePasswordRecoveryFailure(err) {
  return {
    type: types.INITIATE_PASSWORD_RECOVERY_FAILURE,
    err
  }
}

function finishPasswordRecoveryRequest() {
  return {
    type: types.FINISH_PASSWORD_RECOVERY_REQUEST
  }
}

function finishPasswordRecoverySuccess() {
  return {
    type: types.FINISH_PASSWORD_RECOVERY_SUCCESS
  }
}

function finishPasswordRecoveryFailure(err) {
  return {
    type: types.FINISH_PASSWORD_RECOVERY_FAILURE,
    err
  }
}

function passwordChangeRequest(){
  return {
    type: types.PASSWORD_CHANGE_REQUEST
  }
}

function passwordChangeSuccess(){
  return {
    type: types.PASSWORD_CHANGE_SUCCESS
  }
}

function passwordChangeFailure(err){
  return {
    type: types.PASSWORD_CHANGE_FAILURE,
    err
  }
}

export function loginUser(user, location) {
  return (dispatch) => {
    dispatch(loginUserRequest(user))
    return request('post', {...user}, `${apiUrl}/signin`)
    .then(res => {
      dispatch(loginUserSuccess(res))

      if(location.state && location.state.nextPathname)
        browserHistory.push(location.state.nextPathname)
      else browserHistory.push('/')
    })
    .catch(err => {
      dispatch(loginUserFailure(err))
      dispatch(showResponseMessage(err, 'danger'))
    })
  }
}

export function registerUser(user) {
  return (dispatch) => {
    dispatch(registerUserRequest(user))
    return request('post', {...user}, `${apiUrl}/signup`)
    .then(res => {
      dispatch(registerUserSuccess())

      dispatch(showResponseMessage(res, 'success'))
    })
    .catch(err => {
      dispatch(registerUserFailure(err))
      if(typeof err == 'array') dispatch(showResponseMessage("Invalid form data.", 'danger'))
      else dispatch(showResponseMessage(err, 'danger'))
    })
  }
}

export function logoutUser() {
  browserHistory.push('/login')

  return {
    type: types.LOGOUT_USER
  }
}

export function getUsers() {
  return (dispatch, getState) => {
    dispatch(getUsersRequest())
    let { token } = getState().users

    return request('get', {}, apiUrl, token)
    .then(users => {
      dispatch(getUsersSuccess(users))
    })
    .catch(err => {
      dispatch(getUsersFailure(err))

      if(err == 403)
        dispatch(logoutUser())
    })
  }
}

export function updateUser(user) {
  return (dispatch, getState) => {
    dispatch(updateUserRequest(user))
    let { token } = getState().users

    return request('post', {...user}, `${apiUrl}/update`, token)
    .then(user => {
      dispatch(updateUserSuccess(user))

      dispatch(showResponseMessage('User was updated', 'success'))
    })
    .catch(err => {
      dispatch(updateUserFailure(err))

      if(err == 403)
        dispatch(logoutUser())
      else {
        if(typeof err == 'array') dispatch(showResponseMessage("Invalid form data.", 'danger'))
        else dispatch(showResponseMessage(err.message, 'danger'))
      }
    })
  }
}

export function confirmEmail(codeValue) {
  return (dispatch) => {
    dispatch(confirmEmailRequest())
    return request('get', {}, `${apiUrl}/email/confirm/${codeValue}`)
    .then(res => {
      dispatch(confirmEmailSuccess())

      dispatch(showResponseMessage(res, 'success'))
    })
    .catch(err => {
      dispatch(confirmEmailFailure(err))

      dispatch(showResponseMessage(err, 'danger'))
    })
  }
}

export function initiateUserEmailChange(newEmailValue) {
  return (dispatch, getState) => {
    const user = getState().users.currentUser
    dispatch(initiateUserEmailChangeRequest())
    let { token } = getState().users

    return request('post', {...{email: newEmailValue, userId: user.id}}, `${apiUrl}/email/change/initiate`, token)
    .then(res => {
      dispatch(initiateUserEmailChangeSuccess(res))

      dispatch(showResponseMessage(res.message, 'success'))
    })
    .catch(err => {
      dispatch(initiateUserEmailChangeFailure(err))

      if (err != 403) dispatch(showResponseMessage(err.message, 'danger'))

      if(err == 403)
        dispatch(logoutUser())
    })
  }
}

export function continueEmailChange(codeValue) {
  return (dispatch) => {
    dispatch(continueEmailChangeRequest())
    return request('get', {}, `${apiUrl}/email/change/continue/${codeValue}`)
    .then(res => {
      dispatch(continueEmailChangeSuccess())

      dispatch(showResponseMessage(res, 'success'))
    })
    .catch(err => {
      dispatch(continueEmailChangeFailure(err))

      dispatch(showResponseMessage(err, 'danger'))
    })
  }
}

export function finishEmailChange(codeValue) {
  return (dispatch, getState) => {
    dispatch(finishEmailChangeRequest())
    return request('get', {}, `${apiUrl}/email/change/finish/${codeValue}`)
    .then(res => {
      let user = getState().users.currentUser
      user.email = res
      dispatch(finishEmailChangeSuccess(user))

      dispatch(showResponseMessage(`Email was changed to ${res}.`, 'success'))
    })
    .catch(err => {
      dispatch(finishEmailChangeFailure(err))

      dispatch(showResponseMessage(err, 'danger'))
    })
  }
}

export function initiatePasswordRecovery(emailValue) {
  return (dispatch) => {
    dispatch(initiatePasswordRecoveryRequest())
    return request('post', {...{emailValue: emailValue}}, `${apiUrl}/password/recover/initiate`)
    .then(res => {
      dispatch(initiatePasswordRecoverySuccess())

      dispatch(showResponseMessage(res, 'success'))
    })
    .catch(err => {
      dispatch(initiatePasswordRecoveryFailure(err))

      dispatch(showResponseMessage(err, 'danger'))
    })
  }
}

export function finishPasswordRecovery(codeValue, newPasswordValue) {
  return (dispatch) => {
    dispatch(finishPasswordRecoveryRequest())

    return request('post', {...{codeValue: codeValue, newPasswordValue: newPasswordValue}}, `${apiUrl}/password/recover/finish`)
    .then(res => {
      dispatch(finishPasswordRecoverySuccess())

      dispatch(showResponseMessage(res, 'success'))
    })
    .catch(err => {
      dispatch(finishPasswordRecoveryFailure(err))

      dispatch(showResponseMessage(err, 'danger'))
    })
  }
}

export function passwordChange(oldPasswordValue, newPasswordValue) {
  return (dispatch, getState) => {
    const user = getState().users.currentUser
    let { token } = getState().users

    dispatch(passwordChangeRequest())

    return request('post', {...{userId: user.id, oldPasswordValue: oldPasswordValue, newPasswordValue: newPasswordValue}}, `${apiUrl}/password/change`, token)
    .then(res => {
      dispatch(passwordChangeSuccess())

      dispatch(showResponseMessage(res.message, 'success'))
    })
    .catch(err => {
      dispatch(passwordChangeFailure(err))

      if (err != 403) dispatch(showResponseMessage(err.message, 'danger'))

      if(err == 403)
        dispatch(logoutUser())
    })
  }
}

export function restoreSignedInUser() {
  return (dispatch, getState) => {
    var { users } = getState()

    if (!users.token) {
        return
    }

    var tokenData = getDecodedTokenData(users.token)

    if(moment.unix(tokenData.expiryDate).isBefore(moment())){
      return dispatch(logoutUser())
    }
  }
}
