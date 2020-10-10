import { combineReducers } from 'redux';

let locallyStoredFavourites = JSON.parse(localStorage.getItem("favourites") || "[]");
/*
* This maintains a list of favourited/hearted trails for the user.
*/
const hearts = (state = locallyStoredFavourites, action) => {
  let newState = state;
  switch (action.type) {
    case 'HEART_TRAIL_SUCCESS':
    case 'HEART_TRAIL_UNAUTHORIZED':
      newState = [...new Set([...state, action.id])];
      localStorage.setItem("favourites", JSON.stringify(newState));
      break;
    case 'UNHEART_TRAIL_SUCCESS':
    case 'UNHEART_TRAIL_UNAUTHORIZED':
      newState = state.filter(id => id != action.id);
      localStorage.setItem("favourites", JSON.stringify(newState));
      break;
    case 'GET_USER_HEARTS':
      newState = action.response.result;
      // TODO: Make a set and join the user's loclaly stored favourites with
      // the returned lsit of favourites.
      break;
    default:
      return state;
  }
  return newState;
}

const ids = (state = [], action) => {
  switch (action.type) {
    case 'FETCH_TRAILS_SUCCESS':
      return action.response.result;
    case 'FETCH_TRAIL_SUCCESS':
      return [...state.filter(id => id != action.response.result), action.response.result];
    case 'ADD_TRAIL_SUCCESS':
      return [...state, action.response.result];
    case 'COMMIT_TRAIL_EDIT_SUCCESS':
      return [...state, action.response.result];
    default:
      return state;
  }
};

const isFetching = (state = false, action) => {
  switch (action.type) {
    case 'FETCH_TRAIL_REQUEST':
    case 'FETCH_TRAILS_REQUEST':
      return true;
    case 'FETCH_TRAILS_SUCCESS':
    case 'FETCH_TRAIL_SUCCESS':
    case 'FETCH_TRAILS_FAILURE':
    case 'FETCH_TRAIL_FAILURE':
      return false;
    default:
      return state;
  }
};

const errorMessage = (state = null, action) => {
  switch (action.type) {
    case 'FETCH_TRAILS_FAILURE':
      return action.message;
    case 'FETCH_TRAIL_FAILURE':
      return action.message;
    case 'FETCH_TRAIL_REQUEST':
    case 'FETCH_TRAILS_REQUEST':
    case 'FETCH_TRAIL_SUCCESS':
    case 'FETCH_TRAILS_SUCCESS':
      return null;
    default:
      return state;
  }
};

const list = combineReducers({
  ids,
  isFetching,
  errorMessage,
  hearts // TODO: Should this be in a userProfile reducer?
});

export default list;

export const getIds = (state) => state.ids;
export const getIsFetching = (state) => state.isFetching;
export const getErrorMessage = (state) => state.errorMessage;
/**
 * A list of the users favourite trails.
 * @param {*} state 
 */
export const getUserFavourites = (state) => state.hearts;
