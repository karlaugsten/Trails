import React from 'react';
import DumbSeason from './Season';
import DumbDuration from './Duration';
import DumbElevation from './Elevation';
import DumbRating from './Rating';
import DumbDistance from './Distance';
import { getTrail, getIsFetchingId, getErrorMessageForId } from '../../../Reducers';
import { connect } from 'react-redux';
import { withRouter } from 'react-router';

const mapStateToProps = (state, { params, id }) => {
  return {
    isFetching: getIsFetchingId(state, id),
    errorMessage: getErrorMessageForId(state, id),
    trail: getTrail(state, id),
  };
}; 

const wrapLoading = (Component) => class extends React.Component {
  render() {
    if(this.props.isFetching || !this.props.trail) return (<div>----</div>);
    return <Component {...this.props} />
  }
}

/**
 * Order matters below, the order will dictate
 * the order that the stats are rendered in CardStats
 */

export const Elevation = withRouter(connect(
  mapStateToProps,
  null
)(wrapLoading(DumbElevation)));

export const Distance = withRouter(connect(
  mapStateToProps,
  null
)(wrapLoading(DumbDistance)));

export const Duration = withRouter(connect(
  mapStateToProps,
  null
)(wrapLoading(DumbDuration)));

export const Rating = withRouter(connect(
  mapStateToProps,
  null
)(wrapLoading(DumbRating)));

export const Season = withRouter(connect(
  mapStateToProps,
  null
)(wrapLoading(DumbSeason)));

