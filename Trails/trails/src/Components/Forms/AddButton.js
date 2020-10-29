import React from 'react';
import styled, { css, keyframes } from 'styled-components';
import BoxShadow from '../Styles/BoxShadow';

let AddButtonStyle = styled.button`
  background-color: #245f24;
  color: ${props => props.theme.text};
  border-radius: 40px;
  border: none;
  height: ${props => props.height || "30px"};
  width: ${props => props.width || "30px"};
  display: flex;
  align-self: center;
  text-align: center;
  justify-content: center;
  cursor: pointer;
  align-items: center;
  margin: 1em;
  transition: background 0.25s, box-shadow 0.25s, color 0.25s, text-shadow 0.25s ease-in-out;
  ${props => props.clicked ? "" : BoxShadow}
`;

export default class AddButton extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      clicked: false
    }
  }

  

  render() {
    const { children, onClick, ...others } = this.props;
    return (
      <AddButtonStyle clicked={this.state.clicked} onClick={(e) => {this.setState({clicked: true}); onClick(e);}} {...others} >
        <i className="fas fa-plus">
        </i>
      </AddButtonStyle>
    );
  }
};