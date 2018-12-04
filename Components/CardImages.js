//import React from 'react';

class CardImages extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      currentIndex: 0
    }
  }

  componentWillMount() {
    /*setTimeout(() => {
      console.log("setting next");
      this.nextImage();

    }, 3000);*/
  }

  nextImage() {
    this.setState({currentIndex: (this.state.currentIndex+1)%this.props.images.length});
  }

  previousImage() {
    this.setState({currentIndex: this.state.currentIndex == 0 ? this.props.images.length-1 : this.state.currentIndex-1});
  }

  render() {
    console.log("rendering currentIndex " + this.state.currentIndex);
    var imageTransitions = [];
    if(this.props.images.length > 1) {
      imageTransitions.push(<div className="card-image-overlay">
        <i className="fas fa-angle-left pull-left" onClick={() => this.previousImage()}></i>
        <i className="fas fa-angle-right pull-right" onClick={() => this.nextImage()}></i>
      </div>)
    }
    return (
      <div className="card-image-wrapper">
        {imageTransitions}
        {this.props.images.map((image, index) => <img style={{'transform': `translateX(-${400*this.state.currentIndex}px)`}} className="card-image" key={index} src={image} />)}
      </div>
    );
  }
}