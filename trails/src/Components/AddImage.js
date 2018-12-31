import React from 'react';

export default class AddImage extends React.Component {
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
    data.append("trailId", this.props.trailId);
    data.append("editId", this.props.editId);
    // post the image file to the server and return url to onAdd props method
    fetch('/api/images', {
      method: 'POST',
      body: data
    }).then(result => result.json()
    ).catch(error => {
      console.log(error);
      alert(error);
    }).then(image => {
      const { url } = image;
      console.log(url);
      this.props.onAdd(image);
    });
  }

  render() {
    
    return (
      <React.Fragment>
      <form onSubmit={this.handleSubmit}>
        <label>
          Upload image:
          <input type="file" ref={this.fileInput} />
        </label>
        <br />
        <button type="submit">Add</button>
      </form>
      {this.props.images.map(img => (<img style={{height: "50px"}} src={img.url} />))}
      </React.Fragment>
    );
  }
}