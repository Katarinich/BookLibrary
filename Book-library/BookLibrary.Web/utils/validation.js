import revalidator from 'revalidator'

const userDraftSchema = {
  "type": "object",
  "properties": {
    "userName": {"type": "string", "pattern": "^[a-zA-Z0-9_-]{3,16}$", "messages": {"pattern": "UserName can contain only letters and numbers."}},
    "firstName": {"type": "string", "pattern": "^.+$", "messages": {"pattern": "First name can't be empty."}},
    "lastName": {"type": "string", "pattern": "^.+$", "messages": {"pattern": "Last name can't be empty."}},
    "email": {"type": "string", "pattern": "^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$", "messages": {"pattern": "Email is not valid."}},
    "dateOfBirth": {"type": "number", "maximum": new Date().getTime() / 1000, "messages": {"maximum": "Date of birth is not valid."}},
    "mobilePhone": {"type": "string", "pattern": "^(\\+|\\d)[0-9]{7,16}$", "messages": {"pattern": "Mobile phone is not valid."}},
    "country": {"type": "string", "pattern": "^[\0-\uD7FF\uE000-\uFFFF]|[\uD800-\uDBFF][\uDC00-\uDFFF]|[\uD800-\uDBFF](?![\uDC00-\uDFFF])|(?:[^\uD800-\uDBFF]|^)[\uDC00-\uDFFF]{3,16}$", "messages": {"pattern": "Country can contain only letters."}},
    "state": {"type": "string"},
    "city": {"type": "string", "pattern": "^[\0-\uD7FF\uE000-\uFFFF]|[\uD800-\uDBFF][\uDC00-\uDFFF]|[\uD800-\uDBFF](?![\uDC00-\uDFFF])|(?:[^\uD800-\uDBFF]|^)[\uDC00-\uDFFF]{3,16}$", "messages": {"pattern": "City can contain only letters."}},
    "addressLine": {"type": "string", "pattern": "^.+$"},
    "zipcode": {"type": "string", "pattern": "^.+$"},
    "password": {"type": "string", "pattern": "^[a-zA-Z0-9_-]{8,18}$", "messages": {"pattern": "Password can contain only letters and numbers and contains from 8 to 18 characters."}}
  }
}

export function validateUserDraft(userDraft) {
  return revalidator.validate(userDraft, userDraftSchema)
}
