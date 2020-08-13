import React from 'react';
import Image from './Image';
import styled from 'styled-components';

const ImageWrapper = styled.div`
  height: 250px;
  position: relative;
  overflow: hidden;
  display: flex;
  margin: 0 -10px;
  border-top-right-radius: 4px;
  border-top-left-radius: 4px;
  margin-bottom: 10px;
`

const ImageOverlay = styled.div`
  position: absolute;
  top: 100px;
  bottom: 0;
  left: 0;
  right: 0;
  height: 100%;
  width: 100%;
  opacity: 0;
  transition: .3s ease;
  z-index: 200;
  &:hover {
    opacity: 0.5;
  }
`

const TransitionIcon = styled.i`
  font-size: 3em;
  z-index: 200;
  margin: 10px;
`

export default class CardImages extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      currentIndex: 0,
      loaded: [0]
    }
  }

  componentWillMount() {
    /*setTimeout(() => {
      console.log("setting next");
      this.nextImage();

    }, 3000);*/
  }

  nextImage() {
    var nextIndex = (this.state.currentIndex+1)%this.props.images.length;
    this.setState({
      currentIndex: nextIndex,
      loaded: this.state.loaded.includes(nextIndex) ? this.state.loaded : [...this.state.loaded, nextIndex]
    });
  }

  previousImage() {
    var prevIndex = this.state.currentIndex == 0 ? this.props.images.length-1 : this.state.currentIndex-1
    this.setState({
      currentIndex: prevIndex,
      loaded: this.state.loaded.includes(prevIndex) ? this.state.loaded : [...this.state.loaded, prevIndex]
    });
  }

  /**
   * 
   * Start loading the next index on hover
   */
  startLoad(index) {
    this.setState({
      loaded: this.state.loaded.includes(index) ? this.state.loaded : [...this.state.loaded, index]
    });
  }

  render() {
    console.log("rendering currentIndex " + this.state.currentIndex);
    var imageTransitions = [];
    if(this.props.images.length > 1) {
      imageTransitions.push(
        <ImageOverlay>
          <TransitionIcon className="fas fa-angle-left pull-left" onMouseOver={() => this.startLoad(this.state.currentIndex-1)} onClick={() => this.previousImage()}></TransitionIcon>
          <TransitionIcon className="fas fa-angle-right pull-right" onMouseOver={() => this.startLoad(this.state.currentIndex+1)} onClick={() => this.nextImage()}></TransitionIcon>
        </ImageOverlay>
      )
    }
    return (
      <ImageWrapper>
        {imageTransitions}
        {this.props.images.map((image, index) => <Image startLoad={this.state.loaded.includes(index)} style={{'transform': `translateX(-${100*this.state.currentIndex}%)`}} key={index} image={image} />)}
      </ImageWrapper>
    );
  }
}