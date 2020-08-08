import React from 'react';

export default ({ children, ...others }) => (
    <React.Fragment>
      <div {...others}>
        <div className="loader-wrapper">
          <div className="loader"  />
        </div>
      </div>
    </React.Fragment>
  );