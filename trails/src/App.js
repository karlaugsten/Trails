import React from 'react';
import TrailEditor from './Components/TrailEditor';
import TrailNav from './Components/TrailNav';
import LoginModal from './Components/LoginModal';
import ProfileButton from './Components/ProfileButton';
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import ReduxTrailCards from './Components/ReduxTrailCards';
import ReduxFullTrailCard from './Components/ReduxFullTrailCard';
import ProfilePage from './Components/ProfilePage';

export default class App extends React.Component {
    render() {
      return (
        
          <Router>
            <React.Fragment>
              <TrailNav>
                <div className="trail-navbar-button"><Link to="/edit"><i className="fas fa-plus"></i></Link></div>
                <div className="trail-navbar-button"><ProfileButton /></div>
              </TrailNav>
              <LoginModal />
              <div id="container" className="container-fluid" style={{paddingTop: "50px"}}>
                  <Route exact path="/" component={ReduxTrailCards} />
                  <Route path="/trails/:trailId" component={ReduxFullTrailCard} />
                  <Route path="/edit/:editId" component={TrailEditor} />
                  <Route exact path="/edit" component={TrailEditor} />
                  <Route path="/profile" component={ProfilePage} />
              </div>
            </React.Fragment>
          </Router>
      );
    }
  }
  