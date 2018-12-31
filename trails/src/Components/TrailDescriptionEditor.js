import React, { Component } from 'react';
import {
  convertFromRaw,
  convertToRaw,
  EditorState,
  ContentState
} from 'draft-js';

import AddImage from './AddImage';

import 'draft-js-image-plugin/lib/plugin.css';
import 'draft-js/dist/Draft.css';

import Editor, { composeDecorators } from 'draft-js-plugins-editor';

import createImagePlugin from 'draft-js-image-plugin';

import createAlignmentPlugin from 'draft-js-alignment-plugin';

import createFocusPlugin from 'draft-js-focus-plugin';

import createResizeablePlugin from 'draft-js-resizeable-plugin';

import createBlockDndPlugin from 'draft-js-drag-n-drop-plugin';

//import createDragNDropUploadPlugin from 'draft-js-drag-n-drop-upload-plugin';
import editorStyles from './editorStyles.css';
//import mockUpload from './mockUpload';

const resizeablePlugin = createResizeablePlugin();
const alignmentPlugin = createAlignmentPlugin();
const focusPlugin = createFocusPlugin();
const blockDndPlugin = createBlockDndPlugin();
const { AlignmentTool } = alignmentPlugin;

const decorator = composeDecorators(
  resizeablePlugin.decorator,
  alignmentPlugin.decorator,
  focusPlugin.decorator,
  blockDndPlugin.decorator
);
const imagePlugin = createImagePlugin({ decorator });

const plugins = [
  alignmentPlugin,
  resizeablePlugin,
  imagePlugin,
  focusPlugin,
  blockDndPlugin
];

export default class TrailDescriptionEditor extends Component {
  constructor(props) {
    super(props);
    this.state = {
      editorState: EditorState.createEmpty()
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
    this.editor.focus();
  };

  addImage = (image) => {
    this.setState({
      editorState: imagePlugin.addImage(this.state.editorState, image.url)
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
        <div className={editorStyles.editor} onClick={this.focus}>
          <Editor
            editorState={this.state.editorState}
            onChange={this.onChange.bind(this)}
            plugins={plugins}
            ref={(element) => { this.editor = element; }}
          />
          <AlignmentTool />
          <AddImage trailId={this.props.trailId} editId={this.props.editId} images={this.props.images} onAdd={(image) => this.addImage(image)} />
        </div>
      </div>
    );
  }
}