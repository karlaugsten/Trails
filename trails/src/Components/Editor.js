import React, { Component } from 'react';

import 'draft-js-image-plugin/lib/plugin.css';
import 'draft-js/dist/Draft.css';
import 'draft-js-alignment-plugin/lib/plugin.css';
import 'draft-js-inline-toolbar-plugin/lib/plugin.css';


import Editor, { composeDecorators } from 'draft-js-plugins-editor';

import createImagePlugin from 'draft-js-image-plugin';

import createAlignmentPlugin from 'draft-js-alignment-plugin';

import createFocusPlugin from 'draft-js-focus-plugin';

import createResizeablePlugin from 'draft-js-resizeable-plugin';

import createBlockDndPlugin from 'draft-js-drag-n-drop-plugin';

import createInlineToolbarPlugin  from 'draft-js-inline-toolbar-plugin';


const resizeablePlugin = createResizeablePlugin();
const alignmentPlugin = createAlignmentPlugin();
const focusPlugin = createFocusPlugin();
const blockDndPlugin = createBlockDndPlugin();
const toolbarPlugin = createInlineToolbarPlugin({});
const { AlignmentTool } = alignmentPlugin;
//const { Toolbar } =  toolbarPlugin;
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
  toolbarPlugin,
  blockDndPlugin
];

export default class TrailEditor extends Component {
  constructor(props) {
    super(props);
    this.imagePlugin = imagePlugin;
  }

  focus = () => {
    setTimeout(this.editor.focus, 50);
  };

  render() {
    return (
      <div onClick={this.focus}>
            <Editor onClick={this.focus}
              editorState={this.props.editorState}
              onChange={this.props.onChange}
              plugins={plugins}
              readOnly={this.props.readOnly}
              ref={(element) => { this.editor = element; }}
            />
            <AlignmentTool />
          </div>
    );
  }
}