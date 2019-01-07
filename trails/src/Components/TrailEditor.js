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
        editId: props.match.params.editId,
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
    }
  
    componentDidMount() {
      // Load the trail edit...
      /*this.trails.edit(this.props.trailId).then(edit => {

      });*/
      if(this.props.match.params.editId) 
      {
        this.trails.getEdit(this.props.match.params.editId).then(edit => {
          this.setState({
            trailId: edit.trailId,
            editId: edit.editId,
            images: edit.images,
            description: edit.description
          })
        })
      } 
      else 
      {
        this.trails.create().then(edit => {
          this.props.history.push(`/edit/${edit.editId}`)
          this.setState({
            trailId: edit.trailId,
            editId: edit.editId
          })
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