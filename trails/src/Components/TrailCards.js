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
      var trails = this.props.trails.map((t, i) => (<TrailCard trail={t} key={t.trailId} id={t.trailId} />));
      return (
        <div className="card-container" id="card-container">
            {trails}
        </div>
      );
    }
  }