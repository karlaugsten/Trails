import React from 'react';
import styled from 'styled-components';
import withTrail from '../withTrail';
import DraftContent from '../DraftContent';

const CardDescription = styled.div`
font-family: 'National Park';
margin-top: 5px;
margin-bottom: 5px;
font-weight: 100;
font-size: 0.9em;
position: relative;
overflow: hidden;
text-overflow: ellipsis;
height: 200px;
`;

const Description = ({trail}) =>
(
  <CardDescription>
    <DraftContent short content={trail.description} />
  </CardDescription>
);

export default withTrail(Description);