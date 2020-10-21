import React from 'react';
import FileUploader from './Forms/FileUploader';
import styled from 'styled-components';
import BoxShadow from './Styles/BoxShadow';
import DeleteButton from './Forms/DeleteButton';

var ImageContainerWrapper = styled.div`
  text-align: center;
  align-items: center;
  justify-content: center;
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
`

var ImageContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  text-align: center;
  height: 80px;
  width: 128px;
  margin: 5px;
  ${props => props.shadow ? BoxShadow : ""}
`

var ImagesContainer = styled.div`
display: flex;
flex-wrap: wrap;
width: 70%;
margin-left: 15%;
margin-right: 15%;
margin-top: 1.5em;
margin-bottom: 1.5em;
`

export default class AddImage extends React.Component {
  constructor(props) {
    super(props);
    this.fileInput = React.createRef();

    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit = (file) => {
    let data = new FormData()
    data.append("file", file);
    data.append("trailId", this.props.trailId);
    data.append("editId", this.props.editId);
    return fetch('/api/images', {
      method: 'POST',
      body: data
    });
  }

  onAdd = (imagePromise) => {
    imagePromise.then(image => {
      this.props.onAdd(image);
    }).catch(error => {
      // TODO: handle error
    })
  }

  onDelete = (image) => {
    this.props.onDelete(image);
  }

  render() {
    
    return (
      <ImagesContainer>
        {this.props.images.map(img => (
          <ImageContainerWrapper>
            <ImageContainer shadow><img style={{height: "100%"}} src={img.thumbnailUrl} />
            </ImageContainer>
            <DeleteButton onClick={() => this.onDelete(img)}/>
          </ImageContainerWrapper>))
        }
        <ImageContainer>
          <FileUploader upload={this.handleSubmit} finished={this.onAdd}/>
        </ImageContainer>
      </ImagesContainer>
    );
  }
}