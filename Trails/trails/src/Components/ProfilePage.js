import React, { Component } from 'react';
import {connect } from 'react-redux';
import {withRouter } from 'react-router';
import * as actions from '../Actions';
import {getFavouriteTrails, getTrail, getUserName }from '../Reducers';
import TrailCardCarousel from './TrailCardCarousel';

/**
 * Used to display user favourites, user edits, as well as user information/settings.
 */
class ProfilePage extends React.Component {
    constructor(props) {
      super(props);
    }

    componentWillMount(props) {
      //this.props.fetchUserFavourites();
    }

    render() {
      return (
        <>
          <h1>Hello, {this.props.username}</h1>

          <h2>Favourites</h2>
          <div>
            <TrailCardCarousel trails={this.props.favourites} />
          </div>
          <h2>Pending Edits</h2>
          <div>
            <TrailCardCarousel trails={this.props.edits} />
          </div>
        </>
      );
    }
  }

  const mapStateToProps = (state) => {
    return {
      favourites: getFavouriteTrails(state)
      .map(id => getTrail(state, id))
      .filter(t => t !== undefined),
      edits: [], // TODO: Get the users edits.
      username: getUserName(state)
    };
  };
  
  ProfilePage = withRouter(connect(
    mapStateToProps,
    actions
  )(ProfilePage));
  
  export default ProfilePage;

