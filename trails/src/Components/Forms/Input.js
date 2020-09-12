import React from 'react';
import styled from 'styled-components';

const StyledField = styled.div`
width: 80%;
display: flex;
flex-flow: column-reverse;
margin-top: 1em;
margin-bottom: 1em;
margin-left: 10%;
margin-right: 10%;
color: ${props => props.theme.text};
border-color: ${props => props.error ? props.theme.errorText : props.theme.text}${props => props.float ? "" : "80"};
`;

const StyledLabel = styled.label`
  color: ${props => props.error ? props.theme.errorText : "inherit"};
  transform: ${props => props.float ? "scale(0.6) translateX(-33%)" : "translateY(1em)"};
  font-size: 1.1em;
  opacity: ${props => props.float ? "70%" : "70%"};
  letter-spacing 0.1em;
  transition: transform .5s, opacity .5s, font-size 0.5s ease-in-out;
  cursor: text;
  white-space: nowrap;
  height: 1.1em;
  align-self: start;
`;

const StyledInput = styled.input`
  border-bottom: ${props => props.float ? "2px" : "1px"} solid;
  border-top: none;
  border-left: none;
  border-right: none;
  border-color: inherit;
  color: inherit;
  background-color: transparent;
  outline: none;
  height: auto;
  cursor: text; 
  font-size: 1em;
  &::placeholder {
    opacity: none;
  }
  &:hover {
  }
  &:-webkit-autofill {
    background-color: transparent !important;
    color: ${props => props.theme.text};
  }
`;

export default class Input extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selected: false,
      valid: true
    }
  }

  componentDidMount = () => {
    if(this.props.value || this.props.value === 0) {
      this.setState({
        selected: true
      });
    }
  }

  handleChange = (event, onChange) => {
    this.setState({
      value: event.target.value,
      selected: this.state.selected || (!!event.target.value || event.target.value === 0),
      valid: !this.props.validate || this.props.validate(event.target.value)
    });
    
    onChange(event);
  }

  render() {
    const { placeholder, onChange, style, ...others } = this.props;
    const { valid } = this.state;
    return (
      <StyledField style={style} float={this.state.selected} error={!valid}>
        <StyledInput 
          float={this.state.selected}
          error={!valid}
          id={placeholder + "-id"} 
          onFocus={() => this.setState({selected: true})} 
          onBlur={() => this.setState({selected: !!this.props.value || this.props.value === 0})} 
          onChange={(event) => this.handleChange(event, onChange)}
          value={this.props.value}
          {...others} 
        />
        <StyledLabel error={!valid} for={placeholder + "-id"} float={this.state.selected}>{placeholder}</StyledLabel>
      </StyledField>
    );
  }
};