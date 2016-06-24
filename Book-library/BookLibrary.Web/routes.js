import React from 'react'
import { Route, IndexRoute, browserHistory } from 'react-router'

import App from './containers/App'
import Profile from './containers/Profile'
import MainSection from './containers/MainSection'
import LoginSection from './containers/LoginSection'
import EmailChangeMessage from './containers/EmailChangeMessage'
import EmailConfirmMessage from './containers/EmailConfirmMessage'
import PasswordRecoveryForm from './containers/PasswordRecoveryForm'
import EmailChangeConfirmMessage from './containers/EmailChangeConfirmMessage'

export default (store) => {
  const requireAuth = (nextState, replace, callback) => {
    const { users: { currentUser } } = store.getState()
    if (!currentUser) {
      replace({
        pathname: '/login',
        state: { nextPathname: nextState.location.pathname }
      })
    }
    callback()
  }

  return (
    <Route component={ App }>
      <Route path="/" component={ MainSection } onEnter={ requireAuth } >
        <Route path="profile/:userId" component={ Profile } />
        <Route path="email-change/:codeValue" component={ EmailChangeMessage } />
        <Route path="email-change-confirm/:codeValue" component={ EmailChangeConfirmMessage } />
      </Route>
      <Route path="login" component={ LoginSection } />
      <Route path="email-confirm/:codeValue" component={ EmailConfirmMessage } />
      <Route path="password-recover/:codeValue" component={ PasswordRecoveryForm } />
    </Route>
  )
}
