import React from 'react';
import { getTrail, getIsFetchingId, getErrorMessageForId } from '../Reducers';
import { connect } from 'react-redux';
import { withRouter } from 'react-router';

const mapStateToProps = (state, { id }) => {
  return {
    isFetching: getIsFetchingId(state, id),
    errorMessage: getErrorMessageForId(state, id),
    trail: getTrail(state, id),
  };
};

/**
 * Wraps a compnent by passing in the trail as a prop.
 */
const withTrail = (Component) => 
  withRouter(connect(
    mapStateToProps,
    null
  )(Component));

export default withTrail;