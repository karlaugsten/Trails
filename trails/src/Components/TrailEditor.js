import React from 'react';
import TrailDescriptionEditor from './TrailDescriptionEditor';
import TrailService from '../Services/TrailService';
import throttle from 'lodash/throttle'

export default class TrailEditor extends React.Component {
    constructor(props) {
      super(props);
      this.trails = new TrailService();
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

      this.save = throttle((trailId, editId, edit) => this.trails.save(trailId, editId, edit).then(edit => {
        console.log("Saved edit: ");
        console.log(edit);
      }), 3000);

      this.handleChange = this.handleChange.bind(this);
    }
  
    componentDidMount() {
      // Load the trail edit...
      /*this.trails.edit(this.props.trailId).then(edit => {

      });*/
      if(this.props.match.params.editId) 
      {
        this.trails.getEdit(this.props.match.params.editId).then(edit => {
          this.setState(edit)
        })
      } 
      else 
      {
        // This creates a whole new trail.
        this.trails.create().then(edit => {
          this.props.history.push(`/edit/${edit.editId}`)
          this.setState(edit)
        })
      }
    }

    componentWillUpdate(nextProps, nextState) {
      if(nextState.editId && nextState.trailId) {
        this.save(nextState.trailId, nextState.editId, nextState);
      }
    }
  
    componentWillUnmount() {
    }

    handleChange = (e) => {
      this.setState({ [e.target.name]: e.target.type === 'number' ? Number(e.target.value) : e.target.value });
    }

    updateDescription = (desc) => {
      this.setState({
          description: desc
        });
    }
  
    render() {
        if(!this.state.trailId || !this.state.editId) {
          return (<div className="card-fullscreen"></div>)
        }
        return (
          <div className="card-fullscreen">
            <div class="form-group">
              <label for="titleInput">Title</label>
              <input type="text" id="titleInput" class="form-control" onChange={this.handleChange} name="title" value={this.state.title} />
            </div>
            <div class="form-group">
              <label for="locationInput">Location</label>
              <input type="text" id="locationInput" class="form-control" onChange={this.handleChange} name="location" value={this.state.location} />
            </div>
            <div class="form-group">
              <label for="distanceInput">Distance (km)</label>
              <input type="number" id="distanceInput" step="1" class="form-control" onChange={this.handleChange} name="distance" value={this.state.distance} />
            </div>
            <div class="form-group">
              <label for="elevationInput">Elevation (m)</label>
              <input type="number" id="elevationInput" step="1" class="form-control" onChange={this.handleChange} name="elevation" value={this.state.elevation} />
            </div>
            <div class="form-group">
              <div class="row">
                <label>Duration</label>
              </div>
              <div class="row">

                <div class="col col-xs-offset-2 col-xs-1">
                  <label for="minDurationInput">From (hr)</label>
                </div>
                <div class="col col-xs-1">
                  <input type="number" id="minDurationInput" step="1" class="form-control" onChange={this.handleChange} name="minDuration" value={this.state.minDuration} max="24" min="0"  />
                </div>
                <div class="col col-xs-1">
                  <label for="minDurationInput">To (hr)</label>
                </div>
                <div class="col col-xs-1">
                  <input type="number" id="ratingInput" step="1" class="form-control" onChange={this.handleChange} name="maxDuration" value={this.state.maxDuration} max="24" min="0"  />
                </div>
              </div>
            </div>
            <div class="form-group">
              <div class="row">
                <label>Season</label>
              </div>
              <div class="row">

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
              </div>
            </div>
            <div class="form-group">
              <label for="ratingInput">Rating (out of 5)</label>
              <input type="number" id="ratingInput" step="1" class="form-control" onChange={this.handleChange} name="rating" value={this.state.rating} max="5" min="0"  />
            </div>
            <TrailDescriptionEditor 
              trailId={this.state.trailId}
              editId={this.state.editId}
              images={this.state.images} 
              addImage={(image) => this.setState({images: this.state.images.concat([image])})} 
              description={this.state.description}
              updateDescription={(desc) => this.updateDescription(desc)}
            />
          </div>
        )
    }
  }