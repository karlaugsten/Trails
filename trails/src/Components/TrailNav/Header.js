import React from 'react';
import styled from 'styled-components';
import { Link } from 'react-router-dom';

const NavbarHeader = styled.div`
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  float: left !important;
  font-size: 2em;
  margin: 2px;
  & > a {
    color: ${props => props.theme.text};
    opacity: 50%;
    text-decoration: none;
    transition: all 250ms cubic-bezier(0.4, 0, 0.2, 1) 0ms;
    padding: 5px;
    border-radius: 4px;
    letter-spacing: 4px;
  }
  & > a:hover {
    color: ${props => props.theme.text};
    opacity: 80%;
    text-decoration: none;
    background-color: rgba(63, 63, 63, 0.28);
  }
`;

export default () => (
  <NavbarHeader>
    <Link to="/"><i className="fas fa-mountain"></i>Trails</Link>
  </NavbarHeader>
);