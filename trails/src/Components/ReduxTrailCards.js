import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import * as actions from '../Actions';
import { getTrails, getErrorMessage, getIsFetching } from '../Reducers';
import TrailCards from './TrailCards';

class ReduxTrailCards extends Component {
  componentDidMount() {
    this.fetchData();
  }

  fetchData() {
    const { fetchTrails } = this.props;
    fetchTrails();
  }

  render() {
    const { isFetching, errorMessage, trails } = this.props;
    if (isFetching && !trails.length) {
      return (
        <TrailCards
          trails={[null, null, null, null]}
        />
      )
    }
    if (errorMessage && !trails.length) {
      return (
        <p>An error occurred. {errorMessage}</p>
      );
    }

    return (
      <TrailCards
        trails={trails.map(t => t.trailId)}
      />
    );
  }
}

const mapStateToProps = (state, { params }) => {
  return {
    isFetching: getIsFetching(state),
    errorMessage: getErrorMessage(state),
    trails: getTrails(state),
  };
};

ReduxTrailCards = withRouter(connect(
  mapStateToProps,
  actions
)(ReduxTrailCards));

export default ReduxTrailCards;
