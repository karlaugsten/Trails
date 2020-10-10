import React from 'react';
import Stat from './Stat';
import CardIcon from './CardIcon';
import Units from './Units';

const Distance = ({trail}) => (
  <Stat title="Total distance">
    {trail.distance}<Units>km</Units> <CardIcon className="fas fa-long-arrow-alt-right"></CardIcon>
  </Stat>
)

export default Distance;