import React from 'react';
import SvgBlur from './SvgBlur';

export default class Image extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      isError: false,
      loaded: false,
    }
    this.image = React.createRef();
  }

  componentWillReceiveProps( nextProps ) {
		// reset the error state if we switch images
		// TODO: support srcsets?
		if ( nextProps.src !== this.props.src ) {
      this.setState( { 
        isError: false, 
        loaded: false 
      } );
		}
  }
  
  componentDidMount() {
    const img = this.image.current;
    if (img && img.complete) {
        this.loaded();
    }
}

	handleError = () => {
		this.setState( { isError: true } );
  };
  
  loaded = () => {
    this.setState({ loaded: true });
  }

  render() {
    const {image, style, startLoad, ...others } = this.props;
    const loadingStyle = {display: "none", position: "absolute", width: "0", height: "0"};

    const imageStyle = {
      height: "100%",
      width: "100%"
    };

    let img = startLoad ? (
        <img 
          style={!this.state.loaded ? loadingStyle : imageStyle} 
          ref={this.image} 
          onLoad={ this.loaded } 
          onError={ this.handleError } 
          className="" 
          src={image.thumbnailUrl} />
      ) : <></>;

    if(this.state.isError) return null;

    const loadingIconStyle = {
      display: "flex",
      alignItems: "center",
      justifyContent: "center",
      height: "100%"
    };

    if(!this.state.loaded && !image.base64Preview) {
      return (
        <>
          <div class="card-image" style={{...style, width: "400px", textAlign:"center", verticalAlign:"center" }} {...others}>
            <i class="fa-4x fas fa-mountain card-image-loader" style={loadingIconStyle} ></i>
          </div>
          {img}
        </>
      );
    } else if(!this.state.loaded && image.base64Preview) {
      var imageString = "data:image/jpeg;base64," + image.base64Preview;
      return (
        <div class="card-image shine" style={style} {...others}>
          <SvgBlur style={imageStyle} base64Image={imageString} height={250} width={400} />
          {img}
        </div>
        );
    }

    return (
      <div class="card-image" style={style} {...others}>
        {img}
      </div>
    );
  }
}