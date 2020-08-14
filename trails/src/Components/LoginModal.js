import React, { Component } from 'react';
import ReactDOM from "react-dom";
import {Modal, Button} from 'react-bootstrap';
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import * as actions from '../Actions';
import { getLoginRequested, getLoginError, isLoggedIn } from '../Reducers';
import styled from 'styled-components';

const Overlay = styled.div`
  position: fixed;
  content: "";
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0,.5);
  z-index: 99998;
  transition: transform 0.5s ease-in-out;
`;

const StyledModal = styled.div`
  position: absolute;
  border-radius: 5;
  width: 50%;
  box-shadow: 0 7px 13px 0 rgba(0, 0, 0, 0.2), 0 4px 8px 0 rgba(0, 0, 0,.19)!important;
  z-index: 99999;
  height: 50%;
  top: 25%;
  left: 25%;
  background-color: rgba(22,22,22,1.0);
  transition: transform 0.5s ease-in-out;
`;


class LoginModal extends Component {
  constructor(props) {
    super(props);
    this.state = {
      email: '',
      password: '',
      show: false,
      top: 0,
      left: 0
    }
  }

  componentDidMount() {
    
  }

  componentDidUpdate(prevProps) {
    if(this.props.loginRequested && !prevProps.loginRequested) {
      // Calculate width and height of modal and center it on screen.
      const height = ReactDOM.findDOMNode(this.node_modal).clientHeight;
      const width = ReactDOM.findDOMNode(this.node_modal).clientWidth;
      const windowWidth = window.innerWidth;
      const windowHeight = window.innerHeight;
      this.setState({
          top: Math.max((windowHeight - height) / 2, 0),
          left: Math.max((windowWidth - width) / 2, 0),
      });
      setTimeout(() => this.setState({ show: true }), 100);
    }
  }

  handleClose() {
    // Send an END_LOGIN action.
    const { cancelLogin } = this.props;
    cancelLogin();
  }

  handleLogin() {
    // Send an END_LOGIN action.
    const { submitLogin } = this.props;
    submitLogin(this.state.email, this.state.password);
  }

  handleChange(event) {
    this.setState({[event.target.name]: event.target.value})
  }

  render() {
    const { loginRequested, loginError, loggedIn } = this.props;
    let loginErrorMessage;
    if (loginError) {
      loginErrorMessage = (
        <p>An error occurred. {loginError}</p>
      );
    }

    return (
    <React.Fragment>
    {loginRequested && !loggedIn ? <Overlay /> : null}
    <StyledModal ref={node => {
                      this.node_modal = node;
                  }} style={{ transform: loginRequested && !loggedIn ? null : "translateY(-1500px)", top: this.state.top, left: this.state.left }} onHide={this.handleClose.bind(this)}>
      <Modal.Header closeButton onHide={this.handleClose.bind(this)}>
        <Modal.Title>You must login.</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {loginErrorMessage}
        <form>
          <input type="text" name="email" placeholder="Email" value={this.state.email} onChange={this.handleChange.bind(this)} />
          <input type="password" name="password" placeholder="Password" value={this.state.password} onChange={this.handleChange.bind(this)} />
        </form>
      </Modal.Body>
      <Modal.Footer>
        <Button class="pull-left" onClick={this.handleLogin.bind(this)}>Login</Button>
        <Button class="pull-right" onClick={this.handleClose.bind(this)}>Cancel</Button>
      </Modal.Footer>
    </StyledModal>
    </React.Fragment>
    );
  }
}

const mapStateToProps = (state, { params }) => {
  return {
    loggedIn: isLoggedIn(state),
    loginError: getLoginError(state),
    loginRequested: getLoginRequested(state),
  };
};

LoginModal = withRouter(connect(
  mapStateToProps,
  actions
)(LoginModal));

export default LoginModal;
