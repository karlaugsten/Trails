import React, { Suspense } from 'react';
import FileUploader from './Forms/FileUploader';
import Polyline from './Polyline';
import Row from './Styles/Row';
import Column from './Styles/Column';

const Graph = React.lazy(() => import('./Graph'));

export default class AddGpx extends React.Component {
  constructor(props) {
    super(props);
    this.fileInput = React.createRef();

    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit(file) {
    let data = new FormData()
    data.append("file", file);
    data.append("editId", this.props.editId);
    return fetch('/api/maps', {
      method: 'POST',
      body: data
    });
  }

  onAdd = (map) => {
    this.props.onAdd(map);
  }

  render() {
    return (
      <React.Fragment>
        <Row style={{width: "50%"}}><Column>Add Map:</Column><Column><FileUploader upload={this.handleSubmit} finished={this.onAdd}/></Column></Row>
        <Row>
          {this.props.map.elevationPolyline ? <Polyline polyline={this.props.map.elevationPolyline}>
              {elevation => 
                  <Suspense fallback={<div>Loading...</div>}>
                      <Graph values={elevation}/>
                  </Suspense>
              }
          </Polyline> : null}
        </Row>
      </React.Fragment>
    );
  }
}