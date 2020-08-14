import React from 'react';
import styled from 'styled-components';
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
`;

const LoadingTextWrapper = styled.div`
  align-items: center;
  display: flex;
  width: 100%;
  padding-left: 15%;
`;

const Subtitle = ({trail}) => 
!trail ? 
(
  <LoadingTextWrapper>
    <LoadingText width="70%" height="22px" innerHeight="12px" />
  </LoadingTextWrapper>
) :
(
  <CardSubtitle>
    <i className="fas fa-map-marker-alt"></i> {trail.location}
  </CardSubtitle>
);

export default withTrail(Subtitle);