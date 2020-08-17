import React from 'react';
import styled from 'styled-components';

export default styled.div`
margin-left: 15px;
margin-right: 15px;
padding: 5px;
transition: ease-in-out 0.2s all;
width: auto;
white-space: nowrap;
height: 35px;
text-align: center;
vertical-align: middle;
&:hover {
  transform: scale(1.2,1.2);
  text-shadow: 0 8px 17px 0 rgba(0, 0, 0, 0.25), 0 6px 20px 0 rgba(0, 0, 0,.22);
}
`