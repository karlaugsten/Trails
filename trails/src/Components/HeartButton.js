import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import * as actions from '../Actions';
import { getFavouriteTrails }from '../Reducers';
import styled from 'styled-components';

const Heart = styled.div`
  height: 50px;
  width: 50px;
  justify-content: center;
  align-items: center;
  display: flex;
  position: absolute;
  z-index: 100;
  top: 0px;
  right: 0px;
  border-radius: 100px;
  margin: 2px;
  border-color: white;
  border-width: 4px;
  transition: all 250ms cubic-bezier(0.4, 0, 0.2, 1) 0ms;
  cursor: pointer;
  font-size: 2.5em;
  font-weight: 200;
  color: rgba(70, 70, 70, 0.71);
  margin-right: 5px;
  &:hover {
    transform: scale(1.2,1.2);
  }

`

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
            <Heart style={{color: isFavourite ? "#e00000cc" : ""}} onClick={this.clicked}>
              <i className={isFavourite ? "fas fa-heart" : "far fa-heart"}></i>
            </Heart>
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