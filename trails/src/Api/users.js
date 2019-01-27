export default class UsersApi {


  static login(email, password) {
    return fetch('/api/users/login', {
      method: 'POST',
      body: JSON.stringify({
        email, 
        password,
      }),
      headers: {
        "Content-Type": "application/json"
      }
    }).then(result => result.json()
    );
  }
}