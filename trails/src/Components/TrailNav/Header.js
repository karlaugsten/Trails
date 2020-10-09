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

const LinkContainer = styled.div`
  height: 54px;
  display: flex;
  width: auto;
  margin-left: 0.5em;
  align-items: center;
  text-align: center;
  vertical-align: center;
  position: relative;
  color: ${props => props.theme.text}AA;
  &:hover {
    color: ${props => props.theme.text};
  }
  &:hover:after {
    height: 3px;
    background-color: ${props => props.theme.text}; 
  }
  &::after {
    content: "";
    background-color: ${props => props.theme.text}AA; 
    position: absolute;
    content: '';
    height: ${props => props.selected ? "2px" : ""};
    bottom: -2px;
    margin: 0 auto;
    left: 0;
    right: 0;
    width: 100%;
  }
`;

const Link = styled.a`
  color: inherit;
  font-size: 0.7em;
  text-decoration: none;
  transition: all 250ms cubic-bezier(0.4, 0, 0.2, 1) 0ms;
  padding: 5px;
  border-radius: 0px;
  letter-spacing: 4px;
  border-bottom: 2px solid;
  border-color: transparent;
  
  padding-bottom: 0.5em;
  padding-top: 0.3em;
  align-self: vertical;
`;

const Header = (props) => (
  <NavbarHeader>
    <i style={{marginRight: "20px"}} className="fas fa-mountain"></i>
    <LinkContainer selected={props.location.pathname == "/"}><Link selected={props.location.pathname == "/"} href="/">Trails</Link></LinkContainer>
    <LinkContainer selected={props.location.pathname == "/races"}><Link selected={props.location.pathname == "/races"} href="/races">Races</Link></LinkContainer>
  </NavbarHeader>
);

export default withRouter(Header);

