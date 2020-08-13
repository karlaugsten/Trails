import React from 'react';
import Stat from './Stat';
import CardIcon from './CardIcon';

const buildRating = (trail) => {
  var ratingJsx = [];
  for(var i = 0; i < trail.rating; i++) {
    ratingJsx.push(<i className="fa fa-star checked" key={i}></i>);
  }
  for(var i = 0; i < 5-trail.rating; i++) {
    ratingJsx.push(<i className="far fa-star" key={5-i}></i>);
  }
  return ratingJsx;
}

const Rating = ({trail}) => (
  <Stat title="Rating">
    {buildRating(trail)}
  </Stat>
)

export default Rating;