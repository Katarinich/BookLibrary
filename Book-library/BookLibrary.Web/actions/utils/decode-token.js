import jwtDecode from 'jwt-decode'

export default function getDecodedTokenData(token) {
  const decodedToken = jwtDecode(token)
  const expiryDate = decodedToken.exp
  const userId = decodedToken.userId

  const data = {
    userId: userId,
    expiryDate: expiryDate
  }

  return data
}
