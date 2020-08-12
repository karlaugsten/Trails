import React from 'react';
import Image from './Image';

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
      imageTransitions.push(<div className="card-image-overlay">
        <i className="fas fa-angle-left pull-left" onMouseOver={() => this.startLoad(this.state.currentIndex-1)} onClick={() => this.previousImage()}></i>
        <i className="fas fa-angle-right pull-right" onMouseOver={() => this.startLoad(this.state.currentIndex+1)} onClick={() => this.nextImage()}></i>
      </div>)
    }
    return (
      <div className="card-image-wrapper">
        {imageTransitions}
        {this.props.images.map((image, index) => <Image startLoad={this.state.loaded.includes(index)} style={{'transform': `translateX(-${100*this.state.currentIndex}%)`}} key={index} image={image} />)}
      </div>
    );
  }
}