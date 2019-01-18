import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import * as actions from '../Actions';
import { getTrail, getErrorMessageForId, getIsFetchingId } from '../Reducers';
import FullTrailCard from './FullTrailCard';

class ReduxFullTrailCard extends Component {
  componentDidMount() {
    this.fetchData();
  }

  componentDidUpdate(prevProps) {
    if (this.props.match.params.trailId !== prevProps.match.params.trailId) {
      this.fetchData();
    }
  }

  fetchData() {
    if(!this.props.match.params.trailId) return;
    const { fetchTrail } = this.props;
    fetchTrail(this.props.match.params.trailId);
  }

  render() {
    const { isFetching, errorMessage, trail } = this.props;
    if (isFetching && !trail) {
      return <p>Loading...</p>;
    }
    if (errorMessage && !trail) {
      return (
        <p>An error occurred. {errorMessage}</p>
      );
    }
    if(!trail) {
      return <p>Loading...</p>;
    }
    return (
      <FullTrailCard
        trail={trail}
        key={trail.trailId}
      />
    );
  }
}

const mapStateToProps = (state, { match }) => {
  return {
    isFetching: getIsFetchingId(state, match.params.trailId),
    errorMessage: getErrorMessageForId(state, match.params.trailId),
    trail: getTrail(state, match.params.trailId),
  };
};

ReduxFullTrailCard = withRouter(connect(
  mapStateToProps,
  actions
)(ReduxFullTrailCard));

export default ReduxFullTrailCard;
