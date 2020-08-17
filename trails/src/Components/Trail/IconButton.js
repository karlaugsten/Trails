import React from 'react';
import styled from 'styled-components';

const IconButon = styled.div`
  z-index: 1000;
  cursor: pointer;
  position: absolute;
  right: 0;
  top: 0;
  width: 35px;
  height: 35px;
  margin: 5px;
  border-radius: 20px;
  display: inline-flex;
  justify-content: center;
  align-content: center;
  align-items: center;
  background-color: rgba(63, 63, 63, 0.0);
  transition: background-color 150ms cubic-bezier(0.4, 0, 0.2, 1) 0ms;
  color: rgba(0, 0, 0, 0.34);
  &:hover {
    background-color: rgb(49 49 49 / 68%);
  }
  & > i {
    font-size: 20px;
  }
`;

export default ({children, ...others}) => <IconButon {...others}>{children}</IconButon>
