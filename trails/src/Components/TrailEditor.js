import React from 'react';
import TrailDescriptionEditor from './TrailDescriptionEditor';
import AddGpx from './AddGpx';
import { TextInput, NumberInput } from './Forms';
import * as actions from '../Actions';
import { getTrailEdit, isLoggedIn, getLoginRequested } from '../Reducers';
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import throttle from 'lodash/throttle'
import styled from 'styled-components';

const FullTrailStyle = styled.div`
  z-index: 9999; 
  width: 90%; 
  height: auto; 
  position: relative;
  background-color: ${props => props.theme.background};

  justify-content: center;
  text-align: center;
  margin: 30px;
  padding: 10px 10px 10px 10px;
  border-radius: 4px;
  box-shadow: 0 8px 17px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0,.19)!important;
  transition: all 0.25s cubic-bezier(0.4, 0, 0.2, 1) 0ms;
`;

const FormStyle = styled.div`
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
`;


const FormGroup = styled.div`
  display: flex;
  flex-wrap: nowrap;
  flex-direction: row;
`;

const LabelStyle = styled.div`
margin-top: 1em;
margin-bottom: 1em;
margin-left: 10%;
margin-right: -5%;
flex: 1 1 auto;
font-size: 1.5em;
`

class TrailEditor extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        images: [],
        description: null,
        editId: Number(props.match.params.editId),
        title: '',
        location: '',
        distance: 0,
        elevation: 0,
        minDuration: 0,
        maxDuration: 0,
        rating: 0
      }

      this.save = throttle((trailId, editId, edit) => this.props.saveTrailEdit(trailId, editId, edit).then(edit => {
        
      }), 10000);

      this.handleChange = this.handleChange.bind(this);
    }
  
    componentDidMount() {
      this.createTrailEdit();
    }

    componentWillUpdate(nextProps, nextState) {
      if(nextProps.isLoggedIn && !this.props.isLoggedIn) {
        this.createTrailEdit();
      }
      if(nextState.editId && nextState.trailId) {
        this.save(nextState.trailId, nextState.editId, nextState);
      }
    }
  
    componentWillUnmount() {
    }

    createTrailEdit = () => {
      if(this.props.match.params.editId) 
      {
        this.props.getTrailEdit(this.props.match.params.editId).then(result => {
          this.setState(result);
        }, error => {

        });
      } 
      else 
      {
        this.props.addTrail().then(edit => {
          this.props.history.replace(`/edit/${edit.editId}`)
        }, error => {

        });
      }
    }

    handleChange = (e) => {
      this.setState({ [e.target.name]: e.target.type === 'number' ? Number(e.target.value) : e.target.value });
    }

    updateDescription = (desc) => {
      this.setState({
          description: desc
        });
    }

    addMap = (map) => {
      this.setState({
        map: map
      });
    }
  
    render() {
        if(!this.props.isLoggedIn) {
          return (<div className="card-fullscreen">Please Log In</div>)
        }
        if(!this.state.trailId || !this.state.editId ) {
          return (<div className="card-fullscreen"></div>)
        }
        return (
          <FullTrailStyle>
            <FormStyle>
              <TextInput style={{fontSize:"1.8em", flexBasis: "100%"}} type="text" placeholder="Title" onChange={this.handleChange} name="title" value={this.state.title} />
              <TextInput style={{fontSize:"0.8em", flexBasis: "100%"}} type="text" placeholder="Location" onChange={this.handleChange} name="location" value={this.state.location} />
              <FormGroup>
                <NumberInput style={{flex: "1 1 auto"}} placeholder="Distance (km)" onChange={this.handleChange} name="distance" value={this.state.distance} />
                <NumberInput style={{flex: "1 1 auto"}} placeholder="Elevation (m)" onChange={this.handleChange} name="elevation" value={this.state.elevation} />
              </FormGroup>
              <FormGroup>
                <LabelStyle>
                  <label>Duration:</label>
                </LabelStyle>
                <NumberInput style={{flex: "1 1 auto"}} placeholder="From (hr)" onChange={this.handleChange} name="minDuration" value={this.state.minDuration} />
                <NumberInput style={{flex: "1 1 auto"}} placeholder="To (hr)" onChange={this.handleChange} name="maxDuration" value={this.state.maxDuration} />
              </FormGroup>
              <FormGroup>
                <LabelStyle>
                  <label>Season:</label>
                </LabelStyle>
                <TextInput style={{flex: "1 1 auto"}} type="text" placeholder="From" onChange={this.handleChange} name="minSeason" value={this.state.minSeason} />
                <TextInput style={{flex: "1 1 auto"}} type="text" placeholder="To" onChange={this.handleChange} name="maxSeason" value={this.state.maxSeason} />

                {/**<div class="row">

                  <div class="col col-sm-offset-2 col-sm-1">
                    <label for="minDurationInput">From</label>
                  </div>
                  <div class="col col-sm-1">
                    <select name="minSeason" onChange={this.handleChange} value={this.state.minSeason}>
                      <option value="January">January</option>
                      <option value="February">February</option>
                      <option value="March">March</option>
                      <option value="April">April</option>
                      <option value="May">May</option>
                      <option value="June">June</option>
                      <option value="July">July</option>
                      <option value="August">August</option>
                      <option value="September">September</option>
                      <option value="October">October</option>
                      <option value="November">November</option>
                      <option value="December">December</option>
                    </select>
                  </div>
                  <div class="col col-sm-1">
                    <label for="minDurationInput">To</label>
                  </div>
                  <div class="col col-sm-1">
                    <select name="maxSeason" onChange={this.handleChange} value={this.state.maxSeason}>
                      <option value="January">January</option>
                      <option value="February">February</option>
                      <option value="March">March</option>
                      <option value="April">April</option>
                      <option value="May">May</option>
                      <option value="June">June</option>
                      <option value="July">July</option>
                      <option value="August">August</option>
                      <option value="September">September</option>
                      <option value="October">October</option>
                      <option value="November">November</option>
                      <option value="December">December</option>
                    </select>
                  </div>
                </div>*/}
              </FormGroup>

              <NumberInput style={{width:"20%"}} placeholder="Rating (out of 5)" onChange={this.handleChange} name="rating" value={this.state.rating} />
            </FormStyle>
            <AddGpx trailId={this.state.trailId} editId={this.state.editId} map={this.state.map} onAdd={(map) => this.addMap(map)} />
            <TrailDescriptionEditor 
              trailId={this.state.trailId}
              editId={this.state.editId}
              images={this.state.images} 
              addImage={(image) => this.setState({images: this.state.images.concat([image])})} 
              description={this.state.description}
              updateDescription={(desc) => this.updateDescription(desc)}
            />
          </FullTrailStyle>
        )
    }
  }

  const mapStateToProps = (state, { match }) => {
    return {
      edit: getTrailEdit(state, match.params.editId),
      isLoggedIn: isLoggedIn(state)
    };
  };
  
  TrailEditor = withRouter(connect(
    mapStateToProps,
    actions
  )(TrailEditor));
  
  export default TrailEditor;