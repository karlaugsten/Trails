import React from 'react';
import {convertFromRaw, Editor, EditorState} from 'draft-js';
import contentStyles from './contentStyles.css';
 
export default class DraftContent extends React.Component {
    constructor(props) {
      super(props);
      
    }

    render() {
        const { content } = this.props;
        const editorJSON = EditorState.createWithContent(convertFromRaw(JSON.parse(content)));
        return (
            <Editor className="something-something"
            editorState={editorJSON}
            readonly={true}
          />
        );
    }
  }