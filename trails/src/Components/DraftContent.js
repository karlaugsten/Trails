import React from 'react';
import {convertFromRaw, Editor, EditorState} from 'draft-js';
import TrailEditor from './Editor';

export default class DraftContent extends React.Component {
    constructor(props) {
      super(props);
      
    }

    render() {
        const { content, short } = this.props;
        if(short) {
          let firstBlock = JSON.parse(content).blocks[0];
          return firstBlock.text;
        }
        const editorJSON = EditorState.createWithContent(convertFromRaw(JSON.parse(content)));
        return (
            <TrailEditor className="draft-content"
              editorState={editorJSON}
              readOnly={true}
          />
        );
    }
  }