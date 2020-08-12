import React from 'react';

export default ({width, height, base64Image, ...others}) => (
  <svg {...others} xmlns="http://www.w3.org/2000/svg"
    xmlnsXlink="http://www.w3.org/1999/xlink"
    width={width} height={height}
    viewBox={"0 0 " + width + " " + height}>
      <filter id="blur" filterUnits="userSpaceOnUse" colorInterpolationFilters="sRGB">
        <feGaussianBlur stdDeviation="18 18" edgeMode="duplicate" />
        <feComponentTransfer>
          <feFuncA type="discrete" tableValues="1 1" />
        </feComponentTransfer>
      </filter>
      <image filter="url(#blur)"
              xlinkHref={base64Image}
              x="0" y="0"
              height="100%" width="100%"/>
  </svg>
) 