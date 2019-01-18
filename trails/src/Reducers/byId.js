import { combineReducers } from 'redux';

const isFetching = (state = {}, action) => {
  switch (action.type) {
    case 'FETCH_TRAIL_REQUEST':
      return {
        ...state,
        [action.id]: true 
      };
    case 'FETCH_TRAIL_SUCCESS':
    case 'FETCH_TRAIL_FAILURE':
      return {
        ...state,
        [action.id]: false 
      };
    default:
      return state;
  }
};

const errorMessage = (state = {}, action) => {
  switch (action.type) {
    case 'FETCH_TRAIL_SUCCESS':
    case 'FETCH_TRAIL_REQUEST':
      return {
        ...state,
        [action.id]: null 
      };
    case 'FETCH_TRAIL_FAILURE':
      return {
        ...state,
        [action.id]: action.message 
      };
    default:
      return state;
  }
};

const byIds = (state = {}, action) => {
  if (action.response) {
    return {
      ...state,
      ...action.response.entities.trails,
    };
  }
  return state;
};

const byId = combineReducers({
  byId: byIds,
  isFetching: isFetching,
  errorMessage: errorMessage,
})

export default byId;

export const getTrail = (state, id) => state.byId[id];
export const getIsFetchingForId = (state, id) => state.isFetching[id];
export const getErrorMessageForId = (state, id) => state.errorMessage[id];
