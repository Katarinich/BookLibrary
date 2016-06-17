import React from 'react'
import { render } from 'react-dom'
import { Provider } from 'react-redux'

//import configureStore from './store/configureStore'
import App from './containers/App'

render(
    <App/>,
  document.getElementById('root')
)
