import React from 'react';
import ZingChart from 'zingchart-react';

export default class Graph extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        points: []
      }
      
    }

    render() {
        const { values } = this.props;
        var maxValue = Math.max(...values);
        var minValue = Math.min(...values);
        let myConfig = {
            type: 'area',
            backgroundColor:'none',
            borderColor: 'none',
            height: "300px",
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
                step: 20, // Step has to match the interpolation step we do for elevation during GPX processing
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
                values: maxValue + ":" + minValue,
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
                    values: this.props.values,
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