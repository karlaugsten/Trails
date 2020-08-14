import TrailCard from './TrailCard';
import React from 'react';

export default class TrailCards extends React.Component {
    constructor(props) {
      super(props);
    }
  
    componentDidMount() {
    }
  
    componentWillUnmount() {
    }
  
    render() {
      var trails = this.props.trails.map((id, i) => (<TrailCard id={id} />));
      return (
        <div className="card-container" id="card-container">
            {trails}
        </div>
      );
    }
  }