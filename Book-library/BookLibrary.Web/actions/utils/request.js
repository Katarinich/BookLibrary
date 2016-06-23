import fetch from 'isomorphic-fetch'

export default function createRequestPromise(method, data, endpoint) {
  const requestHasData = method === 'post' || method === 'delete'
  const props = {
    method,
    credentials: 'same-origin',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    }
  }
  if (requestHasData)
    props['body'] = JSON.stringify(data)
  return fetch(endpoint, props)
  .then(checkStatus)
  .then(res => (requestHasData) ? res.json() : res.text())
}

function checkStatus(res) {
  if (res.status >= 200 && res.status < 400)
    return res
  else {
    var json = res.json()
    return json.then(err => {
      err.status = res.statusText
      throw err
    })
  }
}
