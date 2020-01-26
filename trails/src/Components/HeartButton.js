import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import * as actions from '../Actions';
import {getFavouriteTrails }from '../Reducers';

class HeartButton extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
      }
      
    }

    clicked = () => {
      if(!this.props.isFavourite) this.props.heartTrail(this.props.id);
      else this.props.unHeartTrail(this.props.id)
    }

    render() {
        const {isFavourite} = this.props;
        return (
            <div style={{color: isFavourite ? "#e00000cc" : ""}} onClick={this.clicked} className="card-heart-button">
              <i className={isFavourite ? "fas fa-heart" : "far fa-heart"}></i>
            </div>
        );
    }
  }

  const mapStateToProps = (state, {id}) => {
    return {
      isFavourite: getFavouriteTrails(state).find(favId => favId == id) != null
    };
  };
  
  HeartButton = withRouter(connect(
    mapStateToProps,
    actions
  )(HeartButton));
  
  export default HeartButton;