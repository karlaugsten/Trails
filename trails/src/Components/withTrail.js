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
const withTrail = (Component) => class extends React.Component {
  render() {
    const ConnectedComponent = withRouter(connect(
      mapStateToProps,
      null
    )(Component));
    return <ConnectedComponent {...this.props} />
  }
}

export default withTrail;