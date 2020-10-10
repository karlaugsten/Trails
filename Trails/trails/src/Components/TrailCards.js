import TrailCard from './Card';
import React from 'react';
import CardContainer from './Card/Container';

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
        <CardContainer>
            {trails}
        </CardContainer>
      );
    }
  }