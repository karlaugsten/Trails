import React, { Component } from 'react';

import 'draft-js-image-plugin/lib/plugin.css';
import 'draft-js/dist/Draft.css';

import Editor, { composeDecorators } from 'draft-js-plugins-editor';

import createImagePlugin from 'draft-js-image-plugin';

import createAlignmentPlugin from 'draft-js-alignment-plugin';

import createFocusPlugin from 'draft-js-focus-plugin';

import createResizeablePlugin from 'draft-js-resizeable-plugin';

import createBlockDndPlugin from 'draft-js-drag-n-drop-plugin';

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

export default class TrailEditor extends Component {
  constructor(props) {
    super(props);
    this.imagePlugin = imagePlugin;
  }

  focus = () => {
    this.editor.focus();
  };

  render() {
    return (
          <Editor onClick={this.focus}
            editorState={this.props.editorState}
            onChange={this.props.onChange}
            plugins={plugins}
            readOnly={this.props.readOnly}
            ref={(element) => { this.editor = element; }}
          />
    );
  }
}