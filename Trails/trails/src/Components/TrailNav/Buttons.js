import React from 'react';
import styled from 'styled-components';
import ProfileButton from './ProfileButton';
import { Link } from 'react-router-dom';

const StyledButtons = styled.div`
  float: right !important;
  display: flex;
  height: 100%;
  width: auto;
`;

const StyledButton = styled.div`
  height: 50px;
  width: 50px;
  justify-content: center;
  align-items: center;
  display: flex;
  border-radius: 100px;
  margin: 2px;
  border-color: white;
  border-width: 4px;
  transition: all 250ms cubic-bezier(0.4, 0, 0.2, 1) 0ms;
  cursor: pointer;
  font-size: 2.5em;
  font-weight: 200;
  color: rgba(83, 83, 83, 0.397);
  margin-right: 5px;
  &:hover {
    background-color: rgba(75, 74, 74, 0.363);
    color: rgba(100, 100, 100, 0.774);
  }
  & > a {
    text-decoration: none;
    color: inherit;
    display: inherit;
    align-content: inherit;
    justify-content: inherit;
    align-items: inherit;
  }
`;

export default () => (
  <StyledButtons>
    <StyledButton>
      <Link to="/edit"><i className="fas fa-plus"></i></Link>
    </StyledButton>
    <StyledButton>
      <ProfileButton />
    </StyledButton>
  </StyledButtons>
);