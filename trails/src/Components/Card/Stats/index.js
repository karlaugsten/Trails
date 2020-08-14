import React from 'react';
import DumbSeason from './Season';
import DumbDuration from './Duration';
import DumbElevation from './Elevation';
import DumbRating from './Rating';
import DumbDistance from './Distance';
import LoadingText from '../LoadingText';
import withTrail from '../../withTrail'
import styled from 'styled-components';

/**
 * Wraps the withTrail connector, and displays some text if the component is loading.
 * @param {} Component 
 */
const wrapLoading = (Component) => withTrail(class extends React.Component {
  render() {
    if(this.props.isFetching || !this.props.trail) return (<LoadingText width="70px" height="35px" innerHeight="14px"  id={Component.name}></LoadingText>);
    return <Component {...this.props} />
  }
});

/**
 * Order matters below, the order will dictate
 * the order that the stats are rendered in CardStats
 */
export const Elevation = wrapLoading(DumbElevation);

export const Distance = wrapLoading(DumbDistance);

export const Duration = wrapLoading(DumbDuration);

export const Rating = wrapLoading(DumbRating);

export const Season = wrapLoading(DumbSeason);

