import { normalize } from 'normalizr';
import * as schema from './schema';
import TrailService from '../Services/TrailService';
import { getIsFetchingId, getIsFetching, getTrail } from '../Reducers';

export const fetchTrails = () => (dispatch, getState) => {
  if (getIsFetching(getState())) {
    return Promise.resolve();
  }

  dispatch({
    type: 'FETCH_TRAILS_REQUEST'
  });

  return TrailService.getAll().then(
    response => {
      dispatch({
        type: 'FETCH_TRAILS_SUCCESS',
        response: normalize(response, schema.arrayOfTrails),
      });
    },
    error => {
      dispatch({
        type: 'FETCH_TRAILS_FAILURE',
        message: error.message || 'Something went wrong.',
      });
    }
  );
};

/**
 * Don't know if we need this one.
 * @param {int} id The id of the trail
 */
export const fetchTrail = (id) => (dispatch, getState) => {
  if(getTrail(getState(), id) != null ||
      getIsFetchingId(getState())
      ) { // Trail is already cached.
    return Promise.resolve();
  }

  dispatch({
    type: 'FETCH_TRAIL_REQUEST',
    id
  });

  return TrailService.getAll().then(
    response => {
      dispatch({
        type: 'FETCH_TRAIL_SUCCESS',
        id,
        response: normalize(response, schema.arrayOfTrails),
      });
    },
    error => {
      dispatch({
        type: 'FETCH_TRAIL_FAILURE',
        id,
        message: error.message || 'Something went wrong.',
      });
    }
  );
};

export const addTrail = () => (dispatch) =>
  TrailService.create().then(response => {
    dispatch({
      type: 'ADD_TRAIL_SUCCESS',
      response: normalize(response, schema.trail),
    });
  });

export const heartTrail = (id) => (dispatch) =>
  TrailService.heartTrail(id).then(response => 
    {
      dispatch({
        type: 'HEART_TRAIL_SUCCESS',
        id,
        response: normalize(response, schema.trail),
      });
    },
    error => 
    {
      // TODO: Check if unauthorized and dispatch a REQUEST_LOGIN action if so.
    }
  );
