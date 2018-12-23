import React from 'react';

export default class Graph extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        points: []
      }
      
    }

    componentWillMount(){
        var points = [];
        var y = 250;
        for(var i = 0; i < 1000; i+=10) {
            points.push({x: i, y: y});
        }
        this.setState({points: points});
        setTimeout(() => {
            this.generatePoints();
        }, 1000);
    }
  
    generatePoints() {
        var points = [



        ]
        var y = 125;
        for(var i = 0; i < 1000; i+=10) {
          points.push({x: i, y: y});
          y += Math.random()*20-10;
          y = Math.max(y, 0);
        }
        this.setState({points: points})
    }

    render() {
        var renderedPoints = this.state.points.map((p, i) => {
            return <div key={i} className="graph-point" style={{top: p.y, left: p.x}} />
        })
        var renderedLines = [...Array(20).keys()].map((p,i) => {
            if(p > 0) {
                return <div key={i} className="graph-y-line" style={{left: `${p*5}%`}} />
            }
        });
        return (
            <div className="graph-wrapper">
            {renderedPoints}
            {renderedLines}
            </div>
        );
    }
  }