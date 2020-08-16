import React, { Suspense } from 'react';
import { Provider } from 'react-redux';
import styled, { keyframes, createGlobalStyle } from 'styled-components';
const App = React.lazy(() => import('./App'));

const GlobalStyle = createGlobalStyle`
  body {
    background-image: linear-gradient(to bottom right, #303030 , #383838);
    color: rgb(233, 232, 232);
    font-family: 'National Park';
    height: 100%;
    min-width: fit-content;
    min-height: 100vh;
  }
`

const fade = keyframes`
0% {opacity: 0.2} 
50% {opacity: 0.6; transform: scale(1.2)}
100% {opacity:0.2}
`;

const shine = keyframes`
0% {filter: brightness(70%)} 
50% {filter: brightness(100%)}
100%{filter: brightness(70%)}
`;

const ImageLoaderStyle = styled.i`
animation: ${fade} 1s, ${shine} 1s;
animation-iteration-count: infinite;
display: flex;
align-items: center;
justify-content: center;
height: 100%;
`;

const LoaderContainer = styled.div`
width: 100%;
height: 100vh;
display: flex;
align-items: center;
justify-content: center;
`;

const Loader = () => 
(
  <LoaderContainer>
    <ImageLoaderStyle  className="fa-4x fas fa-mountain" ></ImageLoaderStyle>
  </LoaderContainer>
);

const Root = ({ store }) => (
  <>
  <GlobalStyle />
  <Suspense fallback={<Loader />}>
    <Provider store={store}>
      <App />
    </Provider>
  </Suspense>
  </>
);

export default Root;
