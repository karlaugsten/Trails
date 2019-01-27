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

const token = (state = null, action) => {
  switch (action.type) {
    case 'LOGIN_SUCCESS':
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

export const getLoginRequested = (state) => state.loginRequested;
export const getLoginError = (state) => state.loginError;
export const isLoggedIn = (state) => state.token != null;

