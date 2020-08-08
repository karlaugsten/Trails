import React from 'react';
import Loader from './Loader';

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
    const {src, style, ...others } = this.props;
    const loadingStyle = {display: "none", position: "absolute", width: "0", height: "0"};

    let img = (<img {...others} style={!this.state.loaded ? loadingStyle : style} ref={this.image} onLoad={ this.loaded } onError={ this.handleError } className="card-image" src={src} />);

    if(this.state.isError) return null;

    const loadingIconStyle = {
      display: "flex",
      alignItems: "center",
      justifyContent: "center",
      height: "100%"
    };

    if(!this.state.loaded) {
      return (
        <>
          <div class="card-image" style={{...style, width: "400px", textAlign:"center", verticalAlign:"center" }} {...others}>
            <i class="fa-4x fas fa-mountain card-image-loader" style={loadingIconStyle} ></i>
          </div>
          {img}
        </>
      );
    }

    return img;
  }
}