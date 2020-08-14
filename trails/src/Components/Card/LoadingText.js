import React from 'react';
import styled, { keyframes } from 'styled-components';

const Grow = keyframes`
0% {
  width: 50%;
}
50% {
  width: 100%;
}
100% {
  width: 50%;
}
`;

const Loading = styled.div`
margin-left: 15px;
margin-right: 15px;
padding: 5px;
width: ${props => props.width};
height: ${props => props.height};
display: flex;
align-items: center;
vertical-align: middle;
`

const LoadingInner = styled.div`
  height: ${props => props.height};
  width: 100%;
  float: left;
  animation-fill-mode: forwards;
  opacity: 10%;
  background-color: white;
  border-radius: 2px;
  animation: ${Grow} 1s infinite;
`;

const LoadingText = ({width, height, innerHeight}) => (
  <Loading width={width} height={height}>
    <LoadingInner height={innerHeight} />
  </Loading>
)

export default LoadingText;