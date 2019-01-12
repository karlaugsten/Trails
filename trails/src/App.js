import React from 'react';
import TrailCards from './Components/TrailCards';
import TrailEditor from './Components/TrailEditor';
import TrailNav from './Components/TrailNav';
import { BrowserRouter as Router, Route, Link } from "react-router-dom";

export default class App extends React.Component {
    render() {
      return (
        
          <Router>
            <React.Fragment>
              <TrailNav>
                <div className="trail-navbar-button"><Link to="/edit"><i className="fas fa-plus"></i></Link></div>
                <div className="trail-navbar-button"><i className="far fa-user-circle"></i></div>
              </TrailNav>
              <div className="container-fluid" style={{paddingTop: "50px"}}>
                  <Route exact path="/" component={TrailCards} />
                  <Route path="/edit/:editId" component={TrailEditor} />
                  <Route exact path="/edit" component={TrailEditor} />
              </div>
            </React.Fragment>
          </Router>
      );
    }
  }
  