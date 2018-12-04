//import React from 'react';

class TrailCard extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      fullscreen: false
    }
  }

  componentDidMount() {
  }

  componentWillUnmount() {
  }

  buildRating() {
    var ratingJsx = [];
    for(var i = 0; i < this.props.trail.stats.rating; i++) {
      ratingJsx.push(<i className="fa fa-star checked" key={i}></i>);
    }
    for(var i = 0; i < 5-this.props.trail.stats.rating; i++) {
      ratingJsx.push(<i className="far fa-star" key={5-i}></i>);
    }
    return ratingJsx;
  }

  setFullScreen(full) {
    this.setState({fullscreen: full}, () => 
      this.props.fullscreen(full)
    )
  }

  render() {
    // TODO: If fullscreen make every card invisible and show a hidden div that is in fullscreen mode.
    var rating = this.buildRating();
    if(!this.state.fullscreen){
      return (
        <div style={{display: this.props.hidden ? "none" : ""}} onClick={() => this.setFullScreen(true)} className={this.state.fullscreen ? "card card-fullscreen" : "card"}>
          
            <CardImages images={this.props.trail.images} />
            <div className="card-title">
                {this.props.trail.title}
            </div>
            <div className="card-sub-title">
                <i className="fas fa-map-marker-alt"></i> {this.props.trail.location}
            </div>
            <div className="card-stats">
                <div className="card-stat">{this.props.trail.stats.elevation}<span className="units">m</span> <i className="card-icon fas fa-long-arrow-alt-up"></i></div>
                <div className="card-stat">{this.props.trail.stats.distance}<span className="units">km</span> <i className="card-icon fas fa-long-arrow-alt-right"></i></div>
                <div className="card-stat">{this.props.trail.stats.time}<span className="units">h</span> <i className="card-icon far fa-clock"></i></div>
                <div className="card-stat">{rating}</div>
                <div className="card-stat">{this.props.trail.stats.season} <i className="card-icon far fa-calendar-check"></i></div>
            </div>
            <div className="card-description">
              {this.props.trail.summary}
            </div>

        </div>
      );
    } else{
      return (<div className={this.state.fullscreen ? "card-fullscreen" : "card"}>
        <div onClick={() => this.setFullScreen(false)} className="pull-right" style={{mouse: "hover", width: "30px", height: "30px", borderRadius: "15px", backgroundColor:"lightgrey"}}><i className="fas fa-times"></i></div>
      </div>)
    }
  }
}