import React from 'react';
import CardImages from './CardImages';
import { withRouter } from "react-router-dom";

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
      
      <div className="card" id={`trail-${this.props.trail.trailId}`}>
          <CardImages images={this.props.trail.edit.images.map(i => i.thumbnailUrl)} />
          <div onClick={() => this.handleClick()}>
            <div className="card-title">
                {this.props.trail.title}
            </div>
            <div className="card-sub-title">
                <i className="fas fa-map-marker-alt"></i> {this.props.trail.location}
            </div>
            <div className="card-stats">
                <div title="Elevation gain" className="card-stat">{this.props.trail.elevation}<span className="units">m</span> <i className="card-icon fas fa-long-arrow-alt-up"></i></div>
                <div title="Total distance" className="card-stat">{this.props.trail.distance}<span className="units">km</span> <i className="card-icon fas fa-long-arrow-alt-right"></i></div>
                <div title="Estimated duration" className="card-stat">{this.props.trail.minDuration}-{this.props.trail.maxDuration}<span className="units">h</span> <i className="card-icon far fa-clock"></i></div>
                <div title="Overall rating" className="card-stat">{rating}</div>
                <div title="Best season" className="card-stat">{this.props.trail.minSeason}-{this.props.trail.maxSeason} <i className="card-icon far fa-calendar-check"></i></div>
            </div>
            <div className="card-description">
              {this.props.trail.description || "See more..."}
            </div>
          </div>
      </div>
    );
  }
}

export default withRouter(TrailCard);