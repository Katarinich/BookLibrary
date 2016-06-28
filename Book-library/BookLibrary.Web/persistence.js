const usersKey = 'users'

const descriptors = [
  {
    stateKey: 'users',
    storageKey: usersKey
  }
]

export function loadInitialState() {
  const savedUsers = load(usersKey)
  const users = savedUsers || {}

  var initialState = {
    users: users
  }

  return initialState
}

export const persistChanges = store => next => action => {
  var prevState = store.getState()

  var result = next(action)

  var nextState = store.getState()

  flushStateChanges(prevState, nextState)

  return result
}

function flushStateChanges(prevState = {}, nextState = {}) {
  descriptors.forEach(descriptor => {
    const { stateKey, storageKey } = descriptor

    const prevStateValue = prevState[stateKey]
    const serializedPrevStateValue = serialize(prevStateValue)

    const nextStateValue = nextState[stateKey]
    const serializedNextStateValue = serialize(nextStateValue)

    if (serializedNextStateValue !== serializedPrevStateValue) {
      saveRaw(storageKey, serializedNextStateValue)
    }
  })
}

function serialize(value) {
  return JSON.stringify(value)
}

function deserialize(serializedValue) {
  return JSON.parse(serializedValue)
}

function save(key, value) {
  var serializedValue = serialize(value)
  saveRaw(key, serializedValue)
}

function saveRaw(key, value) {
  localStorage.setItem(key, value)
}

function load(key) {
  var serializedValue = loadRaw(key)
  return deserialize(serializedValue)
}

function loadRaw(key) {
  return localStorage.getItem(key)
}
