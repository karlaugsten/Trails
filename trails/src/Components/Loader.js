import React from 'react';

export default ({ children, ...others }) => (
    <React.Fragment>
      <div className="loader-wrapper" {...others}>
        <div className="loader"  />
      </div>
      {children}
    </React.Fragment>
  );