import React from 'react'
import { render } from 'react-dom'
import { Provider } from 'react-redux'
import { Router, browserHistory } from 'react-router'

import configureStore from './store/configureStore'
import configRoutes from './routes'
import { restoreSignedInUser } from './actions'

const store = configureStore()
const routes = configRoutes(store)
store.dispatch(restoreSignedInUser())

render(
  <Provider store={store}>
    <Router history={browserHistory} routes={routes} />
  </Provider>,
  document.getElementById('root')
)
