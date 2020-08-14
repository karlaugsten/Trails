import React from 'react';
import styled, { keyframes } from 'styled-components';

const spin = keyframes`
to { -webkit-transform: rotate(360deg); }
`;

const Loader = styled.div`
  display: inline-block;
  width: 50px;
  height: 50px;
  border: 3px solid rgba(255,255,255,.3);
  border-radius: 50%;
  border-top-color: #fff;
  animation: ${spin} 1s ease-in-out infinite;
`;

const LoaderWrapper = styled.div`
  position: relative;
  width: 100%;
  height: 100%;
`;

export default ({ children, ...others }) => (
    <React.Fragment>
      <div {...others}>
        <LoaderWrapper>
          <Loader />
        </LoaderWrapper>
      </div>
    </React.Fragment>
  );