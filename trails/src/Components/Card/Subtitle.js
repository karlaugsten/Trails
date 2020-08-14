import React from 'react';
import styled from 'styled-components';
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

const Subtitle = ({trail}) =>
(
  <CardSubtitle>
    <i className="fas fa-map-marker-alt"></i> {trail.location}
  </CardSubtitle>
);

export default withTrail(Subtitle);