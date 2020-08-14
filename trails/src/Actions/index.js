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

export const addTrail = () => (dispatch, getState) =>
  TrailsApi.create().then(response => {
    dispatch({
      type: 'ADD_TRAIL_SUCCESS',
      response: normalize(response, schema.trail),
    });
    return response;
  },
  error => 
  {
    // TODO: Check if unauthorized and dispatch a REQUEST_LOGIN action if so.
    if(error.response.status == 401) {
      login()(dispatch, getState) // Should somehow pop up a login modal.
    }
    return Promise.reject(error);
  });

export const saveTrailEdit = (trailId, editId, trail) => (dispatch, getState) =>
  TrailsApi.save(trailId, editId, trail).then(response => {
    dispatch({
      type: 'SAVE_TRAIL_EDIT_SUCCESS',
      response: normalize(response, schema.trailEdit),
    });
  },
  error => 
  {
    // TODO: Check if unauthorized and dispatch a REQUEST_LOGIN action if so.
    if(error.response.status == 401) {
      login()(dispatch, getState) // Should somehow pop up a login modal.
    }
    return Promise.reject(error);
  });

export const getTrailEdit = (editId) => (dispatch, getState) =>
  TrailsApi.getEdit(editId).then(response => {
    dispatch({
      type: 'FETCH_TRAIL_EDIT_SUCCESS',
      response: normalize(response, schema.trailEdit),
    });
    return response;
  },
  error => 
  {
    // TODO: Check if unauthorized and dispatch a REQUEST_LOGIN action if so.
    if(error.response.status == 401) {
      login()(dispatch, getState) // Should somehow pop up a login modal.
    }
    return Promise.reject(error);
  });

export const createEdit = (trailId) => (dispatch, getState) =>
  TrailsApi.edit(trailId).then(response => {
    dispatch({
      type: 'CREATE_TRAIL_EDIT_SUCCESS',
      response: normalize(response, schema.trailEdit),
    });
    return response;
  },
  error => 
  {
    // TODO: Check if unauthorized and dispatch a REQUEST_LOGIN action if so.
    if(error.response.status == 401) {
      login()(dispatch, getState) // Should somehow pop up a login modal.
    }
    return Promise.reject(error);
  });

export const commitEdit = (trailId, editId) => (dispatch, getState) =>
  TrailsApi.commit(trailId, editId).then(response => {
    dispatch({
      type: 'COMMIT_TRAIL_EDIT_SUCCESS',
      response: normalize(response, schema.trail),
    });
    return response;
  },
  error => 
  {
    // TODO: Check if unauthorized and dispatch a REQUEST_LOGIN action if so.
    if(error.response.status == 401) {
      login()(dispatch, getState) // Should somehow pop up a login modal.
    }
    return Promise.reject(error);
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
      return response;
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

export const heartTrail = (id) => (dispatch, getState) => {
  dispatch({
    type: 'HEART_TRAIL_SUCCESS',
    id
  });
  return TrailsApi.heartTrail(id).then(response => 
    {
      
      return response;
    },
    error => 
    {
      // TODO: Check if unauthorized and dispatch a REQUEST_LOGIN action if so.
      if(error.response.status == 401) {
        dispatch({
          type: 'HEART_TRAIL_UNAUTHORIZED', // This action signifies that we still want to allow the user to 
          // save a subset of favourited trails, even if they are not logged in/dont have an account.
          // We will later merge these heart's once the user logs in or creates an account.
          id
        });
        login()(dispatch, getState) // Should somehow pop up a login modal.
      }
    }
  );
}

  export const unHeartTrail = (id) => (dispatch, getState) =>
  {
    dispatch({
      type: 'UNHEART_TRAIL_SUCCESS',
      id
    });
    return TrailsApi.unHeartTrail(id).then(response => 
      {
        return response;
      },
      error => 
      {
        // TODO: Check if unauthorized and dispatch a REQUEST_LOGIN action if so.
        if(error.response.status == 401) {
          dispatch({
            type: 'UNHEART_TRAIL_UNAUTHORIZED', // This action signifies that we still want to allow the user to 
            // save a subset of favourited trails, even if they are not logged in/dont have an account.
            // We will later merge these heart's once the user logs in or creates an account.
            id
          });
          login()(dispatch, getState) // Should somehow pop up a login modal.
        }
      }
    );
  }

  export const fetchUserFavourites = () => (dispatch, getState) => {
    // TODO: Implement this.
    
  }
