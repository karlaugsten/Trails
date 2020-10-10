import React from 'react';
import styled from 'styled-components';
import withTrail from '../withTrail';
import DraftContent from '../DraftContent';
import LoadingText from './LoadingText';

const CardDescription = styled.div`
font-family: 'National Park';
margin-top: 5px;
margin-bottom: 5px;
font-weight: 100;
font-size: 0.9em;
position: relative;
overflow: hidden;
text-overflow: ellipsis;
height: ${props => props.short ? "170px" : "auto"};
`;

const Description = ({ trail, short }) =>
!trail ?
(<>
  <LoadingText innerHeight="0.7em" width="80%" height="auto" />
  <LoadingText innerHeight="0.9em" width="90%" height="auto" />
  <LoadingText innerHeight="0.9em" width="70%" height="auto" />
  <LoadingText innerHeight="0.9em" width="60%" height="auto" />
  <LoadingText innerHeight="0.9em" width="80%" height="auto" />
</>
) :
(
  <CardDescription short={short}>
    <DraftContent short={short} content={trail.description} />
  </CardDescription>
);

export default withTrail(Description);