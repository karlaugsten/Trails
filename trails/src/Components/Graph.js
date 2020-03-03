import React from 'react';
import ZingChart from 'zingchart-react';

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

        let myConfig = {
            type: 'area',
            backgroundColor:'none',
            borderColor: 'none',
            tooltip: {
                //backgroundColor: 'rgba(0,0,0,0)',
                fontColor: 'var(--fontColor)',
                callout: true,
                padding: '24px 56px',
                borderRadius: 4,
                borderWidth: 1,
                borderColor: 'var(--lightGray)',
                text: '$%v<br>October 13, %kl'
            },
         
            scaleX: {
                minValue: 0,
                step: 100,
                lineWidth: 0,
                tick: {
                    visible: false
                },
                item: {
                    fontSize: '10px',
                    //alpha: '0.4',
                    fontColor: "rgb(233, 232, 232)"
                },
                label: { 
                    text: "Distance (km)",
                    borderColor: 'none',
                    fontColor: 'rgb(233, 232, 232)',
                    fontSize: 14,
                    fontStyle: 'normal',
                    fontWeight: 'normal',
                    padding: '5px 20px'
                        
                }
            },
            scaleY: {
                guide: {
                    lineStyle: 'solid',
                    lineColor: 'var(--gray)'
                },
                lineWidth: 0,
                item: {
                    fontSize: '10px',
                    //alpha: '0.4'
                    fontColor: "rgb(233, 232, 232)"
                },
                tick: {
                    visible: false
                },
                values: '1000:0',
                label: { 
                    text: "Elevation (m)",
                    borderColor: 'none',
                    fontColor: 'rgb(233, 232, 232)',
                    fontSize: 14,
                    fontStyle: 'normal',
                    fontWeight: 'normal',
                    padding: '5px 20px'
                }
            },
            'crosshair-x': {
                'plot-label': {
                    text: "%v meters",
                    decimals: 1
                }
            },
            plot: {
                aspect: 'spline',
                tooltip: { visible: false }
                // Animation docs here:
                // https://www.zingchart.com/docs/tutorials/styling/animation#effect
                /*animation: {
                    effect: 'ANIMATION_EXPAND_BOTTOM',
                    method: 'ANIMATION_STRONG_EASE_OUT',
                    sequence: 'ANIMATION_BY_NODE',
                    speed: 275,
                }*/
            },
            plotarea: {
                backgroundColor: 'transparent'
            },
            series: [
                {
                    // plot 1 values, linear data
                    values: this.state.points.map(p => p.y),
                    backgroundColor: '#3E84F9 #fff',
                    lineColor: 'var(--darkBlue)',
                    lineWidth: 3,
                    alpha: '0.7',
                    marker: {
                    visible: false
                    }
                }
            ]       
        };

        return (
            <div className="graph-wrapper">
                <ZingChart data={myConfig} />
            </div>
        );
    }
  }