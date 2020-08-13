import React from 'react';
import Stat from './Stat';
import CardIcon from './CardIcon';

const Season = ({trail}) => (
  <Stat title="Best season">
    {trail.minSeason}-{trail.maxSeason} <CardIcon className="far fa-calendar-check"></CardIcon>
  </Stat>
)

export default Season;