import React from 'react'
import { Route, IndexRoute, browserHistory } from 'react-router'

import App from './containers/App'
import Profile from './containers/Profile'
import MainSection from './containers/MainSection'
import LoginSection from './containers/LoginSection'

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
      </Route>
      <Route path="login" component={ LoginSection } />
    </Route>
  )
}
