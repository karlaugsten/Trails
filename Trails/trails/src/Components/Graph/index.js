import React from 'react';
import ZingChart from 'zingchart-react';
import styled, { withTheme } from 'styled-components';

const GraphWrapper = styled.div`
  height: 300px;
  position: relative;
  overflow: hidden;
  display: flex;
  margin-bottom: 10px;
  margin-top: 10px;
  width: 100%;
`;

class Graph extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        points: []
      }
      
    }

    render() {
        const { values, theme } = this.props;
        var maxValue = Math.max(...values);
        var minValue = Math.min(...values);
        let graph = {
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
                step: 0.02, // Step has to match the interpolation step we do for elevation during GPX processing
                lineWidth: 0,
                format: "%v",
                decimals: 1,
                tick: {
                    visible: false
                },
                item: {
                    fontSize: '10px',
                    //alpha: '0.4',
                    fontColor: theme.text
                },
                label: { 
                    text: "Distance (km)",
                    borderColor: 'none',
                    fontColor: theme.text,
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
                    fontColor: theme.text
                },
                tick: {
                    visible: false
                },
                values: maxValue + ":" + minValue,
                label: { 
                    text: "Elevation (m)",
                    borderColor: 'none',
                    fontColor: theme.text,
                    fontSize: 14,
                    fontStyle: 'normal',
                    fontWeight: 'normal',
                    padding: '5px 20px'
                }
            },
            'crosshair-x': {
                'plot-label': {
                    text: "%v meters",
                    decimals: 0
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

        let myConfig = {
            gui: {
                behaviors: [
                {
                    id: "Reload",
                    enabled: "none"
                },
                {
                    id: 'ViewSource',
                    enabled: 'none'
                },
                {
                    id: 'ExportData',
                    enabled: 'none'
                },
                {
                    id: 'CrosshairHide',
                    enabled: 'none'
                },
                {
                    id: 'About',
                    enabled: 'none'
                },
                {
                    id: 'ShowGuide',
                    enabled: 'none'
                },
                {
                    id: 'HideGuide',
                    enabled: 'none'
                }
                ]
              },
              graphset: [graph]
        };

        return (
            <GraphWrapper>
                <ZingChart data={myConfig} />
            </GraphWrapper>
        );
    }
  }

  export default withTheme(Graph);