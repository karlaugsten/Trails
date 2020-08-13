import React from 'react';
import CardImages from './CardImages';
import DraftContent from './DraftContent';
import HeartButton from './HeartButton';
import { withRouter } from "react-router-dom";
import styled from 'styled-components';
import CardStats from './Card/CardStats';

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

const CardTitle = styled.div`
  padding: 2px;
  font-size: 30px;
  font-weight: 500;
  font-family: 'National Park';
  text-transform: uppercase;
  letter-spacing: -1.5px;
  position: relative;
  text-overflow: ellipsis;
  white-space: nowrap;
  letter-spacing: 8px;
  flex-grow: 3;
`;

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


const CardDescription = styled.div`
font-family: 'National Park';
margin-top: 5px;
margin-bottom: 5px;
font-weight: 100;
font-size: 0.9em;
position: relative;
overflow: hidden;
text-overflow: ellipsis;
height: 200px;
`;

class TrailCard extends React.Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
  }

  componentWillUnmount() {
  }

  buildRating() {
    var ratingJsx = [];
    for(var i = 0; i < this.props.trail.rating; i++) {
      ratingJsx.push(<i className="fa fa-star checked" key={i}></i>);
    }
    for(var i = 0; i < 5-this.props.trail.rating; i++) {
      ratingJsx.push(<i className="far fa-star" key={5-i}></i>);
    }
    return ratingJsx;
  }

  handleClick() {
    this.props.history.push(`/trails/${this.props.trail.trailId}`);
  }

  render() {
    var rating = this.buildRating();
    return (
      <Card id={`trail-${this.props.trail.trailId}`}>
          <HeartButton id={this.props.trail.trailId} />
          <CardImages images={this.props.trail.images} />
          <div onClick={() => this.handleClick()}>
            <CardTitle>
                {this.props.trail.title}
            </CardTitle>
            <CardSubtitle>
                <i className="fas fa-map-marker-alt"></i> {this.props.trail.location}
            </CardSubtitle>
            <CardStats id={this.props.id}/>
            <CardDescription>
              <DraftContent short content={this.props.trail.description} />
            </CardDescription>
          </div>
      </Card>
    );
  }
}

export default withRouter(TrailCard);