import React, { Component } from 'react';
import ReactDOM from "react-dom";
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import * as actions from '../Actions';
import { getLoginRequested, getLoginError, isLoggedIn } from '../Reducers';
import { TextInput, PasswordInput, Button } from './Forms';
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
  min-width: 300px;
  box-shadow: 0 7px 13px 0 rgba(0, 0, 0, 0.2), 0 4px 8px 0 rgba(0, 0, 0,.19)!important;
  z-index: 99999;
  height: auto;
  min-height: 400px;
  top: 25%;
  left: 25%;
  background-color: ${props => props.theme.background};
  transition: transform 0.5s ease-in-out;
`;

const CenteredContainer = styled.div`
display: flex;
justify-content: center;
align-items: center;
width: 100%;
`;

const Header = styled.div`
font-size: 2em;
margin: 1em;
font-weight: 100;
letter-spacing: 0.15em;
`;

const LoginForm = styled.form`
display: flex;
align-items: center;
flex-direction: column;
`;


class LoginModal extends Component {
  constructor(props) {
    super(props);
    this.state = {
      email: '',
      password: '',
      show: false,
      top: 0,
      left: 0,
      login: true,
      registration: false
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

  validateEmail(email) {
    return email && email.includes("@");
  }

  handleChange(event) {
    this.setState({[event.target.name]: event.target.value})
  }

  handleLoginNav() {
    // do stuff
    this.setState({
      login: true,
      registration: false
    });
  }

  handleSignUp() {
    // do stuff
    this.setState({
      login: false,
      registration: true
    });
  }

  handleRegistration() {
    // do the registration
  }

  render() {
    const { loginRequested, loginError, loggedIn } = this.props;
    let loginErrorMessage;
    if (loginError) {
      loginErrorMessage = (
        <p>An error occurred. {loginError}</p>
      );
    }

    let login = (
    <LoginForm>
      <TextInput style={{maxWidth:"300px"}} validate={this.validateEmail} type="text" name="email" placeholder="Email" value={this.state.email} onChange={this.handleChange.bind(this)} />
      <PasswordInput style={{maxWidth:"300px"}} name="password" placeholder="Password" value={this.state.password} onChange={this.handleChange.bind(this)} />
    </LoginForm>
    );
    let loginHeader = "Please login."
    let registration = (
      <LoginForm>
        <TextInput style={{maxWidth:"300px"}} validate={this.validateEmail} type="text" name="email" placeholder="Email" value={this.state.email} onChange={this.handleChange.bind(this)} />
        <TextInput style={{maxWidth:"300px"}} validate={this.validateEmail} type="text" name="confirmeamil" placeholder="Confirm your email" value={this.state.confirmemail} onChange={this.handleChange.bind(this)} />
        <TextInput style={{maxWidth:"300px"}} validate={this.validateEmail} type="text" name="username" placeholder="Username" value={this.state.username} onChange={this.handleChange.bind(this)} />
        <PasswordInput style={{maxWidth:"300px"}} name="password" placeholder="Password" value={this.state.password} onChange={this.handleChange.bind(this)} />
        <PasswordInput style={{maxWidth:"300px"}} name="confirmpassword" placeholder="Confirm your password" value={this.state.confirmpassword} onChange={this.handleChange.bind(this)} />
      </LoginForm>
    );
    let registrationHeader = "Join us!";

    let loginButton = (<Button primary onClick={this.handleLogin.bind(this)}>Login</Button>);
    let registrationButton = (<Button primary onClick={this.handleRegistration.bind(this)}>Register</Button>);

    let loginNavigation = (<div style={{alignSelf: "flex-end", textAlign: "end", marginTop:"40px"}}><Button style={{fontSize:"0.8em"}} text onClick={this.handleSignUp.bind(this)}>Register</Button></div>);
    let registrationNavigation = (<div style={{alignSelf: "flex-end", textAlign: "start", marginTop:"40px"}}><Button style={{fontSize:"0.8em"}} text onClick={this.handleLoginNav.bind(this)}>Back</Button></div>)

    return (
    <React.Fragment>
    {loginRequested && !loggedIn ? <Overlay /> : null}
    <StyledModal ref={node => {
                      this.node_modal = node;
                  }} style={{ transform: loginRequested && !loggedIn ? null : "translateY(-1500px)", top: this.state.top, left: this.state.left }} onHide={this.handleClose.bind(this)}>
      <CenteredContainer closeButton onHide={this.handleClose.bind(this)}>
        <Header>{this.state.login ? loginHeader: registrationHeader}</Header>
      </CenteredContainer>
      <div>
        {loginErrorMessage}
        {this.state.login ? login: registration}
      </div>
      <CenteredContainer>
        {this.state.login ? loginButton : registrationButton}<Button onClick={this.handleClose.bind(this)}>Cancel</Button>
      </CenteredContainer>
      {this.state.login ? loginNavigation : registrationNavigation}
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
