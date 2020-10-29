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

function enumerateEntities(contentState, callback) {
  var blocks = contentState.getBlocksAsArray();
    
  for(var block in blocks) {
    blocks[block].findEntityRanges(
      (character) => {
        const entityKey = character.getEntity();
        if(entityKey != null) callback(contentState.getEntity(entityKey), entityKey);
        return (
          entityKey !== null &&
          contentState.getEntity(entityKey).getType() === 'IMAGE'
        );
      },
      () => {}
    );
  }
  
}

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

  deleteImage = (image) => {
    enumerateEntities(this.state.editorState.getCurrentContent(), (entity, key) => {
      if(entity.getType() == "IMAGE") {
        var data = entity.getData();
        if (data.src == image.url) {
          var currentContent = this.state.editorState.getCurrentContent();
          var newContent = currentContent.replaceEntityData(key, '');
          const removeEntity = EditorState.push(this.state.editorState, newContent, 'delete-entity');
          const selection = removeEntity.getSelection();
          this.onChange(EditorState.forceSelection(removeEntity, selection));
        }
      }
    });

    this.props.deleteImage(image);
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
        </div>
        <AddImage onDelete={this.deleteImage} trailId={this.props.trailId} editId={this.props.editId} images={this.props.images} onAdd={(image) => this.addImage(image)} />
      </div>
    );
  }
}