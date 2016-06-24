import fetch from 'isomorphic-fetch'

var defaultHeaders = {
  'Accept': 'application/json',
  'Content-Type': 'application/json'
}

export default function createRequestPromise(method, data, endpoint, token) {
  const requestHasData = method === 'post' || method === 'delete'

  var headers = defaultHeaders

  if(token)
    headers['Authorization'] = token

  const props = {
    method,
    credentials: 'same-origin',
    headers: headers
  }
  if (requestHasData)
    props['body'] = JSON.stringify(data)
  return fetch(endpoint, props)
  .then(checkStatus)
  .then(res => res.json())
}

function checkStatus(res) {
  if (res.status >= 200 && res.status < 400)
    return res
  else {
    let status = res.status
    var json = res.json()
    return json.then(err => {
      if(status == 403) err = status
      throw err
    })
  }
}
