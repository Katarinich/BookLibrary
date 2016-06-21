import React, { Component } from 'react';

export default class LoginForm extends Component {

handleSubmit(e){

  var form = document.querySelector('form')

  fetch('/', {
    method: 'POST',
    body: new FormData(form)
  })
}

render(){
    return (
       <form onSubmit={e => this.handleSubmit(e)}>
         <label>Name</label>
         <p><input type="text" name="login" defaultValue="a@mail.ru" /></p>

         <label>Password</label>
         <p><input type="password" name="password" defaultValue="123456" /></p>

         <input type="submit" name="submit" value="Sign In" />
       </form>
  );
}
}
