import React from 'react';
import TrailCards from './Components/TrailCards';

export default class App extends React.Component {
    render() {
      return (
        <div className="container-fluid">
        <h1>Trails</h1>
            <TrailCards />
        </div>
      );
    }
  }
  