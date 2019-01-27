import React from 'react';
import Loader from './Loader';

export default class Image extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      isError: false,
      loaded: false,
    }
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

	handleError = () => {
		this.setState( { isError: true } );
  };
  
  loaded = () => {
    this.setState({ loaded: true });
  }

  render() {
    const {src, ...others } = this.props;
    let img = (<img style={{display: "none"}} onLoad={ this.loaded } onError={ this.handleError } className="card-image" src={src} {...others} />);

    if(this.state.isError) return null;

    if(!this.state.loaded) {
      return (
        <Loader style={{ width: "100%", height: "250px"}}>
          {img}
        </Loader>
      );
    }

    return img;
  }
}