import request from './util/request'
import * as types from '../constants/actionTypes'

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

function logoutUserRequest() {
  return {
    type: types.LOGOUT_USER_REQUEST
  }
}

function logoutUserSuccess() {
  return {
    type: types.LOGOUT_USER_SUCCESS
  }
}

function logoutUserFailure() {
  return {
    type: types.LOGOUT_USER_FAILURE
  }
}

export function loginUser(user) {
  return (dispatch) => {
    dispatch(loginUserRequest(user))
    return request('post', {...user}, `${apiUrl}/signin`)
    .then(res => {
      dispatch(loginUserSuccess(res))
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
    .then(() => {
      dispatch(registerUserSuccess())
    })
    .cathc(err => {
      dispatch(registerUserFailure(err))
    })
  }
}

export function logoutUser() {
  return (dispatch) => {
    dispatch(logoutUserRequest())
    return request('get', {}, `${apiUrl}/logout`)
    .then(() => {
      dispatch(logoutUserSuccess())
    })
    .catch(err => {
      dispatch(logoutUserFailure(err))
    })
  }
}