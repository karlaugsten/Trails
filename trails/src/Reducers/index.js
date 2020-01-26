import { combineReducers } from 'redux';
import byId, * as fromById from './byId';
import list, * as fromList from './trailsList';
import login, * as fromLogin from './login';
import edits, * as fromEdits from './edits';

const trails = combineReducers({
  byId,
  list,
  login,
  edits
});

export default trails;

export const getTrailEdit = (state, id) => fromEdits.getEdit(state.edits, id);

export const getTrailEdits = (state) => fromEdits.getEdits(state.edits);

export const getTrails = (state) => {
  const ids = fromList.getIds(state.list);
  return ids.map(id => fromById.getTrail(state.byId, id));
};

export const getTrail = (state, id) => 
  fromById.getTrail(state.byId, id);

export const getIsFetchingId = (state, id) => 
  fromById.getIsFetchingForId(state.byId, id);

export const getErrorMessageForId = (state, id) => 
  fromById.getErrorMessageForId(state.byId, id);

export const getFavouriteTrails = (state) =>
  fromList.getUserFavourites(state.list);

export const getIsFetching = (state) =>
  fromList.getIsFetching(state.list);

export const getErrorMessage = (state) =>
  fromList.getErrorMessage(state.list);

export const getLoginRequested = (state) =>
  fromLogin.getLoginRequested(state.login);

export const getLoginError = (state) => 
  fromLogin.getLoginError(state.login);

export const isLoggedIn = (state) =>
  fromLogin.isLoggedIn(state.login);

  export const getUserName = (state) =>
  fromLogin.isLoggedIn(state.login) ? fromLogin.getUserName(state.login) : "User"; // some default