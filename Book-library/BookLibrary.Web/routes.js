import React from 'react'
import { Route, IndexRoute } from 'react-router'

import App from './containers/App'
import Profile from './containers/Profile'
import Home from './containers/Home'
import MainSection from './containers/MainSection'

export const routes = (
  <div>
  <Route path='/' component={MainSection}>
  <IndexRoute component={Home} />
    <Route path='profile/:UserId' component={Profile} />
  </Route>
  <Route path='login' component={App} />
  </div>
)
