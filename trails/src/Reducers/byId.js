const byId = (state = {}, action) => {
  if (action.response) {
    return {
      ...state,
      ...action.response.entities.trails,
    };
  }
  return state;
};

export default byId;

export const getTrail = (state, id) => state[id];
