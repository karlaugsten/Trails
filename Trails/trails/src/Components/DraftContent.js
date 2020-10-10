import React from 'react';
import {convertFromRaw, Editor, EditorState} from 'draft-js';
import TrailEditor from './Editor';
import styled from 'styled-components';

const Content = styled.div`
  user-select: none !important;
`;

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
            <Content>
              <TrailEditor
                editorState={editorJSON}
                readOnly={true}
              />
            </Content>
        );
    }
  }