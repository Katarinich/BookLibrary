import React from 'react'
import { render } from 'react-dom'
import { Provider } from 'react-redux'
import { Router, browserHistory } from 'react-router'

import configureStore from './store/configureStore'
import configRoutes from './routes'

const store = configureStore()
const routes = configRoutes(store)

render(
  <Provider store={store}>
    <div>
    <Router history={browserHistory} routes={routes} />
    </div>
  </Provider>,
  document.getElementById('root')
)
