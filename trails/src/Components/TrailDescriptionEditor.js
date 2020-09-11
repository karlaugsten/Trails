import React, { Component } from 'react';
import {
  convertFromRaw,
  convertToRaw,
  EditorState,
  ContentState
} from 'draft-js';

import AddImage from './AddImage';
import TrailEditor from './Editor'

import editorStyles from './editorStyles.css';

import 'draft-js-image-plugin/lib/plugin.css';
import 'draft-js/dist/Draft.css';
import 'draft-js-alignment-plugin/lib/plugin.css';

import createImagePlugin from 'draft-js-image-plugin';

import createAlignmentPlugin from 'draft-js-alignment-plugin';



const alignmentPlugin = createAlignmentPlugin();

const { AlignmentTool } = alignmentPlugin;

const imagePlugin = createImagePlugin();


export default class TrailDescriptionEditor extends Component {
  constructor(props) {
    super(props);
    this.state = {
      editorState: props.description ? this.getEditorState(props.description) : EditorState.createEmpty()
    }
  }

  onChange = (editorState) => {
    this.setState({
      editorState: editorState
    }, () => {
      this.props.updateDescription(JSON.stringify(convertToRaw(editorState.getCurrentContent())));
    })
  };

  focus = () => {
    setTimeout(this.editor.focus, 50);
  };

  addImage = (image) => {
    this.setState({
      editorState: this.editor.imagePlugin.addImage(this.state.editorState, image.url)
    })
    this.props.addImage(image);
  }

  getEditorState(description) {
    if(!description) return EditorState.createEmpty();
    const content = convertFromRaw(JSON.parse(description));
    return EditorState.createWithContent(content);
  }

  componentWillUpdate(nextProps, nextState) {
    if(nextProps.description && !this.props.description){
      this.setState({
        editorState: this.getEditorState(nextProps.description)
      })
    }
  }

  render() {
    //var editorState = this.getEditorState();
    return (
      <div>
        <div className="editor" onClick={this.focus}>
          <TrailEditor
            editorState={this.state.editorState}
            onChange={this.onChange.bind(this)}
            ref={(element) => { this.editor = element; }}
          />
          <AlignmentTool />
          <AddImage trailId={this.props.trailId} editId={this.props.editId} images={this.props.images} onAdd={(image) => this.addImage(image)} />
        </div>
      </div>
    );
  }
}