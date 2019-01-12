import React from 'react';
import { Link } from 'react-router-dom';
import throttle from 'lodash/throttle';

export default class TrailNav extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      top: 0,
      position: 'absolute',
      lastScroll: 0
    }
    //this.handleScroll = throttle(this.handleScroll.bind(this), 100, { trailing: true, leading: true });
    this.handleScroll = this.handleScroll.bind(this);
  }

  componentDidMount() {
    window.addEventListener('scroll', this.handleScroll);
  }

  componentWillUnmount() {
    window.removeEventListener('scroll', this.handleScroll);
  }

  shouldComponentUpdate(nextProps, nextState) {
    return nextState.position != this.state.position || nextState.top != this.state.top;
  }

  handleScroll(event) {
    if(!event.currentTarget) return;
    let scrollY = event.currentTarget.scrollY;
    if(scrollY < this.state.lastScroll && this.state.position != 'fixed' && this.state.top < this.state.lastScroll - 100) {
      this.setState({
        top: scrollY - 100,
        position: 'absolute'
      })
    } 
    else if(scrollY < this.state.lastScroll && this.state.top > scrollY)
    {
      this.setState({
        top: 0,
        position: 'fixed'
      })
    }
    else if(scrollY > this.state.lastScroll && this.state.position == 'fixed'){
      this.setState({
        top: scrollY,
        position: 'absolute'
      })
    }
    this.setState({
      lastScroll: scrollY
    });
  };  

  render() {
    return (
        <div style={{top: this.state.top, position: this.state.position}} className="trail-navbar">
          <div className="trail-navbar-header"><Link to="/"><i className="fas fa-mountain"></i>Trails</Link></div>
          <div className="trail-navbar-buttons">
            {this.props.children}
          </div>
        </div>
    );
  }
}