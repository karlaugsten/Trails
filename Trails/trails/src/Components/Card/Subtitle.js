import React from 'react';
import styled, { css } from 'styled-components';
import LoadingText from './LoadingText';
import withTrail from '../withTrail';

const CardSubtitle = styled.div`
margin-bottom: 10px;
padding: 1px;
font-size: 12px;
font-weight: 100;
position: relative;
text-overflow: ellipsis;
white-space: nowrap;
letter-spacing: 2px;
  ${props => props.big && css`
    font-size: 15px;
    line-height: 10px;
    font-weight: 200;
    flex-grow: 0.5;
  `}
`;

const LoadingTextWrapper = styled.div`
  align-items: center;
  display: flex;
  width: 100%;
  padding-left: 15%;
`;

const Subtitle = ({trail, big}) => 
!trail ? 
(
  <LoadingTextWrapper>
    <LoadingText width="70%" height="22px" innerHeight="12px" />
  </LoadingTextWrapper>
) :
(
  <CardSubtitle big={big}>
    <i className="fas fa-map-marker-alt"></i> {trail.location}
  </CardSubtitle>
);

export default withTrail(Subtitle);