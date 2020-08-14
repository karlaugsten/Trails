import React from 'react';
import CardImages from './CardImages';
import HeartButton from './HeartButton';
import { withRouter } from "react-router-dom";
import styled from 'styled-components';
import CardStats from './Card/CardStats';
import Title from './Card/Title';
import Subtitle from './Card/Subtitle';
import Description from './Card/Description';

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
  cursor: pointer;
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
    this.props.history.push(`/trails/${this.props.trail.trailId}`);
  }

  render() {
    return (
      <Card id={`trail-${this.props.trail.trailId}`}>
          <HeartButton id={this.props.trail.trailId} />
          <CardImages images={this.props.trail.images} />
          <div onClick={() => this.handleClick()}>
            <Title id={this.props.id} />
            <Subtitle id={this.props.id} />
            <CardStats id={this.props.id} />
            <Description id={this.props.id} />
          </div>
      </Card>
    );
  }
}

export default withRouter(TrailCard);