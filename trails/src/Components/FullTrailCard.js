import React, { Suspense } from 'react';
import CardImages from './CardImages';
import DraftContent from './DraftContent';
import { withRouter } from 'react-router-dom';
import Polyline from './Polyline';
// Lazy load graph since zingchart is 600kb!
const Graph = React.lazy(() => import('./Graph'));


class FullTrailCard extends React.Component {
    constructor(props) {
      super(props);
        this.state = {
            className: 'card'
        }
    }
  
    componentDidMount() {
        setTimeout(() => this.setState({
            className: "card-fullscreen"
        }));
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

    exit() {
        this.props.history.push('/');
    }
  
    render() {
        var rating = this.buildRating();
        return (
            <div id="card-container">
            <div className={this.state.className} id={`trail-${this.props.trail.trailId}`}>
                <div onClick={() => this.exit()} className="pull-right icon-button" ><i className="fas fa-times"></i></div>
                <div className="title-wrapper">
                    <div className="card-title">
                        {this.props.trail.title}
                    </div>
                    <div className="card-sub-title">
                        <i className="fas fa-map-marker-alt"></i> {this.props.trail.location}
                    </div>
                </div>
                {/*<CardImages images={this.props.trail.images.map(i => i.url)} /> */}
                
                
                
                <div className="card-stats">
                    <div title="Elevation gain" className="card-stat">{this.props.trail.elevation}<span className="units">m</span> <i className="card-icon fas fa-long-arrow-alt-up"></i></div>
                    <div title="Total distance" className="card-stat">{this.props.trail.distance}<span className="units">km</span> <i className="card-icon fas fa-long-arrow-alt-right"></i></div>
                    <div title="Estimated duration" className="card-stat">{this.props.trail.minDuration}-{this.props.trail.maxDuration}<span className="units">h</span> <i className="card-icon far fa-clock"></i></div>
                    <div title="Overall rating" className="card-stat">{rating}</div>
                    <div title="Best season" className="card-stat">{this.props.trail.minSeason}-{this.props.trail.maxSeason}<i className="card-icon far fa-calendar-check"></i></div>
                </div>
                <Polyline polyline={this.props.trail.map.elevationPolyline}>
                    {elevation => 
                        <Suspense fallback={<div>Loading...</div>}>
                            <Graph values={elevation}/>
                        </Suspense>
                    }
                </Polyline>
                <div className="card-description">
                    <DraftContent content={this.props.trail.description} />
                </div>
            </div>
            </div>
        )
    }
  }

  export default withRouter(FullTrailCard);