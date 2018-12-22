//import React from 'react';

class TrailCardWrapper extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        fullscreen: false
      }
    }
  
    componentDidMount() {
    }
  
    componentWillUnmount() {
    }

    setFullScreen(full) {
      this.setState({fullscreen: full}, () => 
        this.props.fullscreen(full)
      )
    }
  
    render() {
      if(!this.state.fullscreen){
          return (
            <div style={{display: this.props.hidden ? "none" : ""}} className="card">
                <TrailCard trail={this.props.trail} fullscreen={(full) => this.setFullScreen(full)} />
            </div>)
      } else{
          return (
            <div className="card-fullscreen">
                <FullTrailCard fullscreen={(full) => this.setFullScreen(full)} trail={this.props.trail} /> 
            </div>)
      }
    }
  }