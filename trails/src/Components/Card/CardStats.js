import React from 'react';
import styled from 'styled-components';
import * as allStats from './Stats';

const CardStats = styled.div`
display: flex;
flex-wrap: wrap;
width: 100%;
border-bottom: 1px solid #bebebe86;
border-top: 1px solid #bebebe86;
position: relative;
margin-top: 5px;
margin-bottom: 5px;
justify-content: center;
`

/*export default ({id}) =>
  (<CardStats></CardStats>);*/

  export default ({id}) =>
  (
    <CardStats>
      {Object.entries(allStats).map(([statName, Stat], index) => <Stat id={id} key={index} />)}
    </CardStats>
  );