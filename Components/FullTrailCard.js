//import React from 'react';

class FullTrailCard extends React.Component {
    constructor(props) {
      super(props);
    }
  
    componentDidMount() {
    }
  
    componentWillUnmount() {
    }
  
    render() {
        return (
                    <div onClick={() => this.props.fullscreen(false)} className="pull-right icon-button" ><i className="fas fa-times"></i></div>
                )
    }
  }