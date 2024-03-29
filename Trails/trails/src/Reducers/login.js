import { combineReducers } from 'redux';

const loginRequested = (state = false, action) => {
  switch (action.type) {
    case 'REQUEST_LOGIN':
      return true;
    case 'END_LOGIN':
      return false;
    case 'LOGIN_SUCCESS':
      return false;
    default:
      return state;
  }
};

const loginError = (state = null, action) => {
  switch (action.type) {
    case 'LOGIN_SUCCESS':
    case 'REQUEST_LOGIN':
      return null;
    case 'LOGIN_FAILURE':
      return action.message;
    default:
      return state;
  }
};

let initialToken = localStorage.getItem("token") || null;

const token = (state = initialToken, action) => {
  switch (action.type) {
    case 'LOGIN_SUCCESS':
      localStorage.setItem('token', action.token);
      return action.token
    case 'REQUEST_LOGIN':
      return null;
    case 'LOGOUT':
      return null;
    case 'LOGIN_FAILURE':
      return null;
    default:
      return state;
  }
};

const login = combineReducers({
  loginRequested,
  loginError,
  token
})

export default login;

const decode = (token) => JSON.parse(atob(token.split('.')[1]));

const isValidToken = (token) => {
  try {
    const { exp } = decode(token);
    if (Date.now() >= exp * 1000) {
      return false;
    }
  } catch(err) {
    return false;
  }
  return true;
}

export const getLoginRequested = (state) => state.loginRequested;
export const getLoginError = (state) => state.loginError;
export const isLoggedIn = (state) => state.token != null && isValidToken(state.token);
export const getUserName = (state) => state.token != null && decode(state.token).name;

