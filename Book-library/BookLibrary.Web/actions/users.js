import { browserHistory } from 'react-router'

import request from './utils/request'
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
    user
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
    users
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
    user
  }
}

function updateUserFailure(err) {
  return {
    type: types.UPDATE_USER_FAILURE,
    err
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

function initiateUserEmailChangeSuccess() {
  return {
    type: types.INITIATE_USER_EMAIL_CHANGE_SUCCESS
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

function finishEmailChangeSuccess(user) {
  return {
    type: types.FINISH_EMAIL_CHANGE_SUCCESS,
    user
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
      dispatch(showResponseMessage(err.message, 'danger'))
    })
  }
}

export function logoutUser(user) {
  return (dispatch) => {
    dispatch(logoutUserRequest(user))
    return request('post', {...user}, `${apiUrl}/logout`)
    .then(() => {
      dispatch(logoutUserSuccess())
    })
    .catch(err => {
      dispatch(logoutUserFailure(err))
    })
  }
}

export function getUsers() {
  return (dispatch, getState) => {
    dispatch(getUsersRequest())
    let { tokenValue } = getState().users.currentUser

    return request('get', {}, apiUrl, tokenValue)
    .then(users => {
      dispatch(getUsersSuccess(users))
    })
    .catch(err => {
      dispatch(getUsersFailure(err))

      if(err == 403)
        browserHistory.push('/login')
    })
  }
}

export function updateUser(user) {
  return (dispatch) => {
    dispatch(updateUserRequest(user))
    return request('post', {...user}, `${apiUrl}/update`)
    .then(user => {
      dispatch(updateUserSuccess(user))
    })
    .catch(err => {
      dispatch(updateUserFailure(err))

      if(err == 403)
        browserHistory.push('/login')
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

      if(err == 403)
        browserHistory.push('/login')
    })
  }
}

export function initiateUserEmailChange(newEmailValue) {
  return (dispatch, getState) => {
    const user = getState().users.currentUser
    dispatch(initiateUserEmailChangeRequest())
    return request('post', {...{newEmailValue: newEmailValue, userId: user.id}}, `${apiUrl}/email/change/initiate`)
    .then(res => {
      dispatch(initiateUserEmailChangeSuccess())

      dispatch(showResponseMessage(res, 'success'))
    })
    .catch(err => {
      dispatch(initiateUserEmailChangeFailure(err))

      if (err != 403) dispatch(showResponseMessage(err, 'danger'))

      if(err == 403)
        browserHistory.push('/login')
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
    dispatch(passwordChangeRequest())

    return request('post', {...{userId: user.id, oldPasswordValue: oldPasswordValue, newPasswordValue: newPasswordValue}}, `${apiUrl}/password/change`)
    .then(res => {
      dispatch(passwordChangeSuccess())

      dispatch(showResponseMessage(res, 'success'))
    })
    .catch(err => {
      dispatch(passwordChangeFailure(err))

      if (err != 403) dispatch(showResponseMessage(err, 'danger'))

      if(err == 403)
        browserHistory.push('/login')
    })
  }
}
