import { createStore, applyMiddleware, compose } from 'redux';
import createLogger from 'redux-logger';
import thunk from 'redux-thunk';
import trailsApp from './Reducers';

const configureStore = () => {
  const middlewares = [thunk];
  //if (process.env.NODE_ENV !== 'production') {
    middlewares.push(createLogger());
  //}
  
  const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
  return createStore(
    trailsApp,
    composeEnhancers(applyMiddleware(...middlewares))
  );
};

export default configureStore;
