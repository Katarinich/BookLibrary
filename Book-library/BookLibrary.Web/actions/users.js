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
  return (dispatch) => {
    dispatch(getUsersRequest())
    return request('get', {}, apiUrl)
    .then(users => {
      dispatch(getUsersSuccess(users))
    })
    .catch(err => {
      dispatch(getUsersFailure(err))
    })
  }
}

export function updateUser(user) {
  return (dispatch) => {
    dispatch(updateUserRequest)
    return request('post', {...user}, `${apiUrl}/update`)
    .then(user => {
      dispatch(updateUserSuccess(user))
    })
    .catch(err => {
      dispatch(updateUserFailure(err))
    })
  }
}
