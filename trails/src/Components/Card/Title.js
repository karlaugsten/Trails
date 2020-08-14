import React from 'react';
import styled from 'styled-components';
import withTrail from '../withTrail';

const CardTitle = styled.div`
  padding: 2px;
  font-size: 30px;
  font-weight: 500;
  font-family: 'National Park';
  text-transform: uppercase;
  letter-spacing: -1.5px;
  position: relative;
  text-overflow: ellipsis;
  white-space: wrap;
  letter-spacing: 8px;
  flex-grow: 3;
`;

const Title = ({trail}) =>
(
  <CardTitle>
    {trail.title}
  </CardTitle>
);

export default withTrail(Title);