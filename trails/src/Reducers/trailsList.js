import { combineReducers } from 'redux';

const handleHeart = (state, action) => {
  return state; // For now just return state, later on we might want to maintain a list of heaarted trails.
};

const ids = (state = [], action) => {
  switch (action.type) {
    case 'FETCH_TRAILS_SUCCESS':
      return action.response.result;
    case 'ADD_TRAIL_SUCCESS':
      return [...state, action.response.result];
    case 'HEART_TRAIL_SUCCESS':
      return handleHeart(state, action);
    default:
      return state;
  }
};

const isFetching = (state = false, action) => {
  switch (action.type) {
    case 'FETCH_TRAIL_REQUEST':
      return true;
    case 'FETCH_TRAIL_SUCCESS':
    case 'FETCH_TRAIL_FAILURE':
      return false;
    default:
      return state;
  }
};

const errorMessage = (state = null, action) => {
  switch (action.type) {
    case 'FETCH_TRAIL_FAILURE':
      return action.message;
    case 'FETCH_TRAIL_REQUEST':
    case 'FETCH_TRAIL_SUCCESS':
      return null;
    default:
      return state;
  }
};

const list = combineReducers({
  ids,
  isFetching,
  errorMessage,
});

export default list;

export const getIds = (state) => state.ids;
export const getIsFetching = (state) => state.isFetching;
export const getErrorMessage = (state) => state.errorMessage;
