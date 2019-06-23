import { combineReducers } from 'redux';

const ids = (state = [], action) => {
  switch (action.type) {
    case 'CREATE_TRAIL_EDIT_SUCCESS':
      return [...state, action.response.result];
    case 'SAVE_TRAIL_EDIT_SUCCESS':
    case 'COMMIT_TRAIL_EDIT_SUCCESS':
    case 'FETCH_TRAIL_EDIT_SUCCESS':
      return [...state.filter(id => id != action.response.result), action.response.result];
    default:
      return state;
  }
};

const byId = (state = {}, action) => {
  if (action.response) {
    return {
      ...state,
      ...action.response.entities.trailedits,
    };
  }
  return state;
}

const edits = combineReducers({
  ids,
  byId
});

export default edits;

export const getEdits = (state) => 
  state.ids.map(id => state.byId[id]);

export const getEdit =(state, id) =>
  state.byId[id];