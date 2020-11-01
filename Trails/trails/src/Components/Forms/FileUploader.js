import React from 'react';
import styled, { css, keyframes } from 'styled-components';
import AddButton from './AddButton';
import BoxShadow from '../Styles/BoxShadow';

const spin = keyframes`
  to { -webkit-transform: rotate(360deg); }
`;

let Loader = styled.div`
  border-radius: 40px;
  height: 27px;
  width: 27px;
  display: flex;
  align-self: center;
  text-align: center;
  justify-content: center;
  background-color: inherit;
  border: 3px solid rgba(255,255,255,.3);
  border-top: 3px solid #00000080;
  animation: ${spin} 1s ease-in-out infinite;
  margin-left: 1em;
  margin-right: 1em;
`

let Error = styled.div`
  align-text: center;
  font-size: 0.5em;
  color: ${props => props.theme.errorText};
  text-wrap: wrap;
`

export default class FileUploader extends React.Component {
  constructor(props) {
    super(props);

    this.fileInput = React.createRef();
    this.state = {
      processing: false
    };
  }

  onClick = () => {
    this.fileInput.current.click();
  }

  onChange = () => {
    var file = this.fileInput.current.files[0];
    this.setState({
      processing: true,
      error: null
    });
    this.props.upload(file)
      .then(response => {
        if(response.ok)
        {
          return response.json();         
        }
        return Promise.reject('Something went wrong, please try again');
      })
      .then(fileTask => {
        console.log(fileTask);
        this.setState({
          task: fileTask
        });
        this.startPolling();
      }).catch(error => {
        this.setState({
          processing: false,
          error: error.message || "Something went wrong, please try again."
        });
      });
  }

  startPolling = () => {
    this.setState({ timer: setInterval(this.doPoll, 1000)});
  }

  doPoll = () => {
    fetch(this.state.task.callbackUrl, {
      method: 'GET'
    })
    .catch(error => {
      console.log(error);
      this.setState({
        processing: false,
        error: "Something went wrong, please try again."
      });
    })
    .then(result => result.json())
    .then(fileTask => {
      if(fileTask.status == "DONE") {
        clearTimeout(this.state.timer);
        this.setState({
          processing: false,
          task: fileTask,
          error: null
        });
        this.fetchResult(fileTask.finishedUrl);
      } else if(fileTask.status == "ERRORED") {
        clearTimeout(this.state.timer);
        this.setState({
          processing: false,
          task: fileTask,
          error: fileTask.errorMessage
        });
      }
    }).catch(error => {
      console.error(error);
      this.setState({
        processing: false,
        error: "Something went wrong, please try again."
      });
    })
  }

  fetchResult = (url) => {
    this.props.finished(fetch(url, {
      method: 'GET'
    })
    .then(result => result.json()));
  }

  render() {
    const { children, ...others } = this.props;
    const { processing, error } = this.state;
    var button = (
      <AddButton onClick={this.onClick} {...others} />);
    if (processing) {
      button = <Loader /> 
    }
    return (
      <>
        <input onChange={this.onChange} ref={this.fileInput} id="fileinput" type="file" style={{ display: "none" }}/>
        {error ? (<Error>{error}</Error>) : null}
        {button}
      </>
    );
  }
};