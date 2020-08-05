import React from 'react';

export default class Polyline extends React.Component {
  render() {
    const { polyline, isGps } = this.props;
    if(polyline == null) return this.props.children([]);
    var numbers = [];
    var index = 0;
    while(index < polyline.length) {
      var b, shift = 0, result = 0;
      do {
        b = polyline.charCodeAt(index++) - 63;
        result |= (b & 0x1f) << shift;
        shift += 5;
      } while (b >= 0x20);
      numbers.push((result & 1) != 0 ? ~(result >> 1) : (result >> 1))
    }
    if(isGps) {
      // GPS are stored as a flat list of numbers [lat1, long1, lat2_diff, long2_diff]
      // Where the next set of (lat, long) are the difference from the previous lat, long.
      // Confusing right?? Its just to save space. See polyline: TODO add link.

      var gps = numbers.filter((val, i) => i%2==0)
        .map((val, i) => 
          ({latitude: val, longitude: numbers[i*2+1]})
        )
        .reduce((accum, val, i) => i == 0 ? [val] : 
        [
          ...accum, 
          {
            latitude: val.latitude + accum[i-1].latitude,
            longitude: val.longitude + accum[i-1].longitude
          }
        ], []);
      return this.props.children(gps)
    }
    return this.props.children(numbers)
  }
}