import React, { Suspense } from 'react';
import CardImages from '../CardImages';
import DraftContent from '../DraftContent';
import { withRouter } from 'react-router-dom';
import Polyline from '../Polyline';
import CardStats from '../Card/CardStats';
import IconButton from './IconButton';
import styled from 'styled-components';
import Title from '../Card/Title';
import Subtitle from '../Card/Subtitle';
import Description from '../Card/Description';
import CardContainer from '../Card/Container';
// Lazy load graph since zingchart is 600kb!
const Graph = React.lazy(() => import('../Graph'));

const FullTrailStyle = styled.div`
  z-index: 9999; 
  width: 90%; 
  height: auto; 
  position: relative;
  background-color: #424242;

  justify-content: center;
  text-align: center;
  margin: 30px;
  padding: 10px 10px 10px 10px;
  border-radius: 4px;
  box-shadow: 0 8px 17px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0,.19)!important;
  transition: all 0.25s cubic-bezier(0.4, 0, 0.2, 1) 0ms;
`;

class FullTrailCard extends React.Component {
    constructor(props) {
      super(props);
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
        const id = this.props.trail.trailId;
        return (
            <CardContainer>
              <FullTrailStyle id={`trail-${this.props.trail.trailId}`}>
                  <IconButton onClick={() => this.exit()} className="pull-right" ><i className="fas fa-times"></i></IconButton>
                  <div className="title-wrapper">
                      <Title big id={id}/>
                      <Subtitle big id={id} />
                  </div>
                  {/*<CardImages images={this.props.trail.images.map(i => i.url)} /> */}
                  <CardStats id={this.props.trail.trailId}/>
                  <Polyline polyline={this.props.trail.map.elevationPolyline}>
                      {elevation => 
                          <Suspense fallback={<div>Loading...</div>}>
                              <Graph values={elevation}/>
                          </Suspense>
                      }
                  </Polyline>
                  <Description id={id} />
              </FullTrailStyle>
            </CardContainer>
        )
    }
  }

  export default withRouter(FullTrailCard);