import React from 'react';
import Stat from './Stat';
import Units from './Units';
import CardIcon from './CardIcon';

const Season = ({trail}) => (
  <Stat title="Duration">
  {trail.minDuration}-{trail.maxDuration}<Units>h</Units> <CardIcon className="far fa-clock"></CardIcon>
  </Stat>
)

export default Season;