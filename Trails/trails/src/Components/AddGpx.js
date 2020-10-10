import React from 'react';
import Polyline from './Polyline';

export default class AddGpx extends React.Component {
  constructor(props) {
    super(props);
    this.fileInput = React.createRef();

    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit(event) {
    event.preventDefault();
    console.log(
      `Selected file - ${
        this.fileInput.current.files[0].name
      }`
    );
    let data = new FormData()
    data.append("file", this.fileInput.current.files[0]);
    data.append("editId", this.props.editId);
    // post the image file to the server and return url to onAdd props method
    fetch('/api/maps', {
      method: 'POST',
      body: data
    }).then(result => result.json()
    ).catch(error => {
      console.log(error);
      alert(error);
    }).then(map => {
      
      this.props.onAdd(map);
    });
  }

  render() {
    
    return (
      <React.Fragment>
      <form onSubmit={this.handleSubmit}>
        <label>
          Upload GPX:
          <input type="file" ref={this.fileInput} />
        </label>
        <br />
        <button type="submit">Add</button>
      </form>
      </React.Fragment>
    );
  }
}