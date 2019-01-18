import { combineReducers } from 'redux';
import byId, * as fromById from './byId';
import list, * as fromList from './trailsList';

const trails = combineReducers({
  byId,
  list,
});

export default trails;

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

export const getIsFetching = (state) =>
  fromList.getIsFetching(state.list);

export const getErrorMessage = (state) =>
  fromList.getErrorMessage(state.list);
