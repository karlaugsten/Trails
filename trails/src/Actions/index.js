import { normalize } from 'normalizr';
import * as schema from './schema';
import TrailService from '../Services/TrailService';
import { getIsFetching } from '../Reducers';

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

export const addTrail = () => (dispatch) =>
  TrailService.create().then(response => {
    dispatch({
      type: 'ADD_TRAIL_SUCCESS',
      response: normalize(response, schema.trail),
    });
  });

export const heartTrail = (id) => (dispatch) =>
  TrailService.heartTrail(id).then(response => {
    dispatch({
      type: 'HEART_TRAIL_SUCCESS',
      response: normalize(response, schema.trail),
    });
  });
