import React from 'react';
import styled, { css } from 'styled-components';
import BoxShadow from '../Styles/BoxShadow';

let StyledButton = styled.button`
  text-transform: uppercase;
  border: ${props => props.primary || props.text ? "none" : "1px solid " + props.theme.text};
  background: ${props => props.primary ? props.theme.text + "C9" : "none"};
  color: ${props => props.primary ? props.theme.background :  props.theme.text};
  letter-spacing: 0.2em;
  font-size: 1.0em;
  border-radius: 4px;
  cursor: pointer;
  transition: background 0.25s, box-shadow 0.25s, color 0.25s, text-shadow 0.25s ease-in-out;
  margin-left: 0.5em;
  margin-right: 0.5em;
  margin-top: 1em;
  margin-bottom: 1em;
  padding: 0.4em;
  &:hover {
    background: ${props => props.text ? "none" : (props.primary ? props.theme.text : "rgba(0,0,0,0.3)")};
    ${props => props.primary ? BoxShadow : "none"};
    text-shadow: ${props => props.text ? "3px 2px 1px rgba(0,0,0,0.2), 4px 3px 2px rgba(0,0,0,.19)" : "none"};
    ${props => props.text && css`
      color: ${props.theme.text}AA
    `}
  }
`;

export default class Button extends React.Component {
  constructor(props) {
    super(props);
  }

  

  render() {
    const { children, ...others } = this.props;
    return (
      <StyledButton {...others} >
        {children}
      </StyledButton>
    );
  }
};