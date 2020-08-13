import React from 'react';
import Stat from './Stat';
import CardIcon from './CardIcon';
import Units from './Units';

const Elevation = ({trail}) => (
  <Stat title="Elevation gain">
    {trail.elevation}<Units>m</Units> <CardIcon className="fas fa-long-arrow-alt-up"></CardIcon>
  </Stat>
)

export default Elevation;