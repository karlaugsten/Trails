import React from 'react';
import TrailEditor from './Components/TrailEditor';
import TrailNav from './Components/TrailNav';
import LoginModal from './Components/LoginModal';
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import ReduxTrailCards from './Components/ReduxTrailCards';
import ReduxFullTrailCard from './Components/ReduxFullTrailCard';
import ProfilePage from './Components/ProfilePage';
import Body from './Components/Body';
import Container from './Components/Container';

export default class App extends React.Component {
    render() {
      return (
        
          <Router>
            <Body>
              <TrailNav />
              <LoginModal />
              <Container>
                  <Route exact path="/" component={ReduxTrailCards} />
                  <Route path="/trails/:trailId" component={ReduxFullTrailCard} />
                  <Route path="/edit/:editId" component={TrailEditor} />
                  <Route exact path="/edit" component={TrailEditor} />
                  <Route path="/profile" component={ProfilePage} />
              </Container>
            </Body>
          </Router>
      );
    }
  }
  