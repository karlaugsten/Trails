import React from 'react';
import styled, { css } from 'styled-components';
import { withRouter } from 'react-router';

const NavbarHeader = styled.div`
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  float: left !important;
  font-size: 2em;
  margin: 2px;
  border-radius: 0px;
  opacity: 60%;

  & > i {
    margin-right: 10px;
    margin-left: 10px;
  }
`;

const Link = styled.a`
  font-size: 1.0em;
  color: ${props => props.theme.text};
  text-decoration: none;
  transition: all 250ms cubic-bezier(0.4, 0, 0.2, 1) 0ms;
  padding: 5px;
  border-radius: 0px;
  letter-spacing: 4px;
  border-bottom: 2px solid;
  border-color: transparent;
  margin-left: 10px;

  &:hover {
    color: ${props => props.theme.text};
    text-decoration: none;
    border-bottom: 2px solid;
    border-color: ${props => props.theme.text};
  }
  ${props => props.selected && css`
    border-bottom: 2px solid;
    border-color: ${props.theme.text};
  `}
`;

const Header = (props) => (
  <NavbarHeader>
    <i className="fas fa-mountain"></i>
    <Link selected={props.location.pathname == "/"} href="/">Trails</Link>
    <Link selected={props.location.pathname == "/races"} href="/races">Races</Link>
  </NavbarHeader>
);

export default withRouter(Header);

