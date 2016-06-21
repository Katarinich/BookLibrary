import { browserHistory } from 'react-router'

import request from './utils/request'
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
      browserHistory.push('/')
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
    .catch(err => {
      dispatch(registerUserFailure(err))
    })
  }
}

export function logoutUser() {
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
