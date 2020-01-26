import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import * as actions from '../Actions';
import { isLoggedIn, getLoginRequested } from '../Reducers';

class ProfileButton extends Component {

  handleClick() {
    const { isLoggedIn, login } = this.props;
    if(!isLoggedIn) {
      login();
    } else {
      this.props.history.push(`/profile`);
    }
  }

  render() {
    return (
      <i className="far fa-user-circle" onClick={() => this.handleClick()}></i>
    );
  }
}

const mapStateToProps = (state, { params }) => {
  return {
    loginRequested: getLoginRequested(state),
    isLoggedIn: isLoggedIn(state),
  };
};

ProfileButton = withRouter(connect(
  mapStateToProps,
  actions
)(ProfileButton));

export default ProfileButton;
