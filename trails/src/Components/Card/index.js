import React from 'react';
import CardImages from '../CardImages';
import HeartButton from '../HeartButton';
import { withRouter } from "react-router-dom";
import styled from 'styled-components';
import CardStats from './CardStats';
import Title from './Title';
import Subtitle from './Subtitle';
import Description from './Description';
import withTrail from '../withTrail';

const Card = styled.div`
  position: relative;
  background-color: #424242;
  width: 400px;
  height: 600px;
  justify-content: center;
  text-align: center;
  margin: 25px;
  padding: 0px 10px 10px 10px;
  border-radius: 4px;
  box-shadow: 0 8px 17px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0,.19)!important;
  cursor: ${props => props.loading ? "default" : "pointer"};
  &:hover {
    box-shadow: 0 12px 22px 0 rgba(0, 0, 0, 0.2), 0 10px 25px 0 rgba(0, 0, 0,.19)!important;
  }
`;

class TrailCard extends React.Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
  }

  componentWillUnmount() {
  }

  handleClick() {
    if(!this.props.trail) return;
    this.props.history.push(`/trails/${this.props.trail.trailId}`);
  }

  render() {
    const { id, trail } = this.props;
    var maybeTrail = trail || {};
    return (
      <Card loading={!id || !trail} id={`trail-${id}`}>
          <HeartButton id={id} />
          <CardImages images={maybeTrail.images} />
          <div onClick={() => this.handleClick()}>
            <Title id={id} />
            <Subtitle id={id} />
            <CardStats id={id} />
            <Description short id={id} />
          </div>
      </Card>
    );
  }
}

export default withTrail(TrailCard);