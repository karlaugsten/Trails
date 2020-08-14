import React from 'react';
import throttle from 'lodash/throttle';
import styled from 'styled-components';
import Header from './Header';
import Buttons from './Buttons';

const StyledNav = styled.div`
  height: auto;
  width: 100%;
  position: fixed;
  background-color: #313131;
  box-shadow: 0 7px 13px 0 rgba(0, 0, 0, 0.2), 0 4px 8px 0 rgba(0, 0, 0,.19)!important;
  z-index: 99997;
  min-width: fit-content;
`;

export default class TrailNav extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      top: 0,
      position: 'absolute',
      lastScroll: 0,
      startedDown: Infinity
    }
    //this.handleScroll = throttle(this.handleScroll.bind(this), 100, { trailing: true, leading: true });
    this.handleScroll = this.handleScroll.bind(this);
  }

  componentDidMount() {
    window.addEventListener('touchmove', this.handleScroll);
    window.addEventListener('scroll', this.handleScroll);

  }

  componentWillUnmount() {
    window.removeEventListener('touchmove', this.handleScroll);
    window.removeEventListener('scroll', this.handleScroll);
  }

  shouldComponentUpdate(nextProps, nextState) {
    return nextState.position != this.state.position || nextState.top != this.state.top;
  }

  handleScroll(event) {
    if(!event.currentTarget) return;
    let scrollY = event.currentTarget.scrollY;
    let isMobile = window.screen.width < 1280;
    if(scrollY < this.state.lastScroll && this.state.position != 'fixed' && this.state.top < this.state.lastScroll - 100) {
      // Scrolling down.
      this.setState({
        top: scrollY - 100,
        position: 'absolute',
        startedDown: Infinity
      })
    } 
    else if(scrollY < this.state.lastScroll && this.state.top > scrollY)
    {
      // Scrolling up.
      this.setState({
        top: 0,
        position: 'fixed',
        startedDown: Infinity
      })
    }
    else if (scrollY > this.state.lastScroll && this.state.position == 'fixed') {
      // Starting to scroll down!
      if(!isMobile || new Date().getTime() - this.state.startedDown > 500) {
        this.setState({
          top: scrollY,
          position: 'absolute'
        })
      } else {
        this.setState({
          startedDown: new Date().getTime()
        });
      }
    }
    this.setState({
      lastScroll: scrollY
    });
  };  

  render() {
    return (
        <StyledNav style={{left: 0, top: this.state.top, position: this.state.position}}>
          <Header />
          <Buttons />
        </StyledNav>
    );
  }
}