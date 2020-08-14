import React from 'react';
import SvgBlur from './SvgBlur';
import styled, { keyframes, css } from 'styled-components';

const fade = keyframes`
0% {opacity: 0.2} 
50% {opacity: 0.6; transform: scale(1.2)}
100% {opacity:0.2}
`;

const shine = keyframes`
0% {filter: brightness(70%)} 
50% {filter: brightness(100%)}
100%{filter: brightness(70%)}
`;

const ImageStyle = styled.div`
height: 250px;
flex: 1 0 100%;
width: 100%;    
object-fit: cover;
object-position: 0 0;
transition: transform 500ms cubic-bezier(0.455, 0.03, 0.515, 0.955);
${props => props.loading && css`
  animation ${shine} 1s infinite;
`}
`;

const ImageLoaderStyle = styled.i`
animation: ${fade} 1s infinite;
display: flex;
align-items: center;
justify-content: center;
height: 100%;
`;


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
    const {image, style, key, startLoad, ...others } = this.props;
    const loadingStyle = {display: "none", position: "absolute", width: "0", height: "0"};

    const imageStyle = {
      height: "100%",
      width: "100%"
    };
    let imageId = image.thumbnailUrl.split("/").pop().split(".").shift();
    let img = startLoad ? (
        <img 
          id={imageId}
          key={key}
          style={!this.state.loaded ? loadingStyle : imageStyle} 
          ref={this.image} 
          onLoad={ this.loaded } 
          onError={ this.handleError } 
          src={image.thumbnailUrl} />
      ) : <></>;

    if(this.state.isError) return null;

    if(!this.state.loaded && !image.base64Preview) {
      return (
        <>
          <ImageStyle id={"wrapper-" + imageId} style={{...style, width: "400px", textAlign:"center", verticalAlign:"center" }} {...others}>
            <ImageLoaderStyle className="fa-4x fas fa-mountain" ></ImageLoaderStyle>
          </ImageStyle>
          {img}
        </>
      );
    } else if(!this.state.loaded && image.base64Preview) {
      var imageString = "data:image/jpeg;base64," + image.base64Preview;
      return (
        <ImageStyle id={"wrapper-" + imageId} loading style={style} {...others}>
          <SvgBlur style={imageStyle} base64Image={imageString} height={250} width={400} />
          {img}
        </ImageStyle>
        );
    }

    return (
      <ImageStyle id={"wrapper-" + imageId} style={style} {...others}>
        {img}
      </ImageStyle>
    );
  }
}