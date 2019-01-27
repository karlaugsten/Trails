import { normalize } from 'normalizr';
import * as schema from './schema';
import TrailsApi from '../Api/trails';
import UsersApi from '../Api/users';
import { getIsFetchingId, getIsFetching, getTrail, isLoggedIn } from '../Reducers';

export const fetchTrails = () => (dispatch, getState) => {
  if (getIsFetching(getState())) {
    return Promise.resolve();
  }

  dispatch({
    type: 'FETCH_TRAILS_REQUEST'
  });

  return TrailsApi.getAll().then(
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

  return TrailsApi.getAll().then(
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
  TrailsApi.create().then(response => {
    dispatch({
      type: 'ADD_TRAIL_SUCCESS',
      response: normalize(response, schema.trail),
    });
  });

export const login = () => (dispatch, getState) => {
  if(isLoggedIn(getState())) {
    return; // do nothing?
  }
    dispatch({
      type: 'REQUEST_LOGIN', // Should somehow pop up a login modal.
    });
  }

export const submitLogin = (email, password) => (dispatch, getState) => {
  if(isLoggedIn(getState())) {
    return Promise.resolve(); // do nothing?
  }
  return UsersApi.login(email, password).then(
    response => {
      dispatch({
        type: 'LOGIN_SUCCESS',
        token: response.token,
      });
    },
    error => {
      dispatch({
        type: 'LOGIN_FAILURE',
        message: error.message || 'Something went wrong.',
      });
    }
  );
}

export const cancelLogin = () => (dispatch) =>
    dispatch({
      type: 'END_LOGIN', // Cancel the login.
    });

export const heartTrail = (id) => (dispatch) =>
  TrailsApi.heartTrail(id).then(response => 
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
      if(error.statusCode == 401) {
        login() // Should somehow pop up a login modal.
      }
    }
  );
