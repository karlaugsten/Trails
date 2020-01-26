import TrailCard from './TrailCard';
import React from 'react';

const cardWidth = 450;

/**
 * This is a component that will display the trail cards in a carousel,
 * so that the cards only take up one row. This is intended for use on 
 * the user profile, to display a list of "favourite" trails, as well
 * as a list of "completed" trails.
 */
export default class TrailCardCarousel extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
          posX: 0
      };
    }

    hoverLeft = () => this.hover(-1);

    hoverRight = () => this.hover(1);

    hover = (direction) => {
      this.setState({posX: this.state.posX + direction*cardWidth/10})
    }

    moveLeft = () => this.move(-1);

    moveRight = () => this.move(1);

    move = (direction) => {
      // Animate the move 
      this.setState({posX: this.state.posX + direction*cardWidth })
    }
  
    render() {
      var trails = this.props.trails.map((t, i) => (<TrailCard trail={t} key={t.trailId} />));
      var transform = `translateX(${this.state.posX}px)`;
      return (
        <div 

          style={{position: "relative"}} 
          className="card-carousel"
        >
            <div 
              style={{cursor: "pointer", position: "absolute", top: 0, left: 0, height: "100%", width: "25%"}} 
              onHover={this.hoverLeft} 
              onClick={this.moveLeft}
            />
            <div style={{transition: "transform 500ms cubic-bezier(0.455, 0.03, 0.515, 0.955)",  width: "100%", transform: transform, overflowX: "hidden"}}>
              {trails}
            </div>
            <div 
              style={{cursor: "pointer", position: "absolute", top: 0, right: 0, height: "100%", width: "25%"}}
              onHover={this.hoverRight} 
              onClick={() => this.moveRight()} 
             />
        </div>
      );
    }
  }