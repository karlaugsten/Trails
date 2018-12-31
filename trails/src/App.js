import React from 'react';
import TrailCards from './Components/TrailCards';
import TrailEditor from './Components/TrailEditor';
import { BrowserRouter as Router, Route, Link } from "react-router-dom";

export default class App extends React.Component {
    render() {
      return (
        <Router>
          <div className="container-fluid">
          <h1>Trails</h1>
              <Route exact path="/" component={TrailCards} />
              <Route path="/edit/:editId" component={TrailEditor} />
              <Route exact path="/edit" component={TrailEditor} />
          </div>
        </Router>
      );
    }
  }
  