using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Converts a polyline to/from a list of locations.
/// </summary>
public class PolylineConverter {
  private class PolylineIterator : IEnumerable<int>
  {
    private String _polyline;

    public PolylineIterator(String polyline) {
      _polyline = polyline;
    }
    public IEnumerator<int> GetEnumerator()
    {
      int index = 0;
      while(index < _polyline.Length) {
        int b, shift = 0, result = 0;
        do {
          b = _polyline[index++] - 63;
          result |= (b & 0x1f) << shift;
          shift += 5;
        } while (b >= 0x20);
        yield return ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
  }

  public static string Convert(List<Location> points) {
    IEnumerable<Location> newPoints = new List<Location>();
    newPoints = newPoints.Append(points.First());
    for(int index = 1; index < points.Count; index ++) {
      newPoints = newPoints.Append(new Location() {
        Longitude = points[index].Longitude - points[index-1].Longitude,
        Latitude = points[index].Latitude - points[index-1].Latitude
      });
    }

    return String.Join("", newPoints.Select(Convert).ToList());
  }

  public static string Convert(Location point) => Convert(point.Latitude) + Convert(point.Longitude);

  public static string Convert(double value) {
    int tmp = (int)(Math.Round(value*1E5));
    tmp <<= 1;
    if(value < 0.0) tmp = ~tmp;
    IEnumerable<char> result = new List<char>();
    do {
      int v = tmp&0x1f;
      if((tmp>>5) > 0) {
        v |= 0x20;
      }
      v += 63;
      result = result.Append((char)(v));
      tmp = tmp>>5;
    } while(tmp > 0);
    return String.Join("", result);
  }

  public static string Convert(List<int> ints) => String.Join("", ints.Select(Convert));

  public static string Convert(int value) {
    int tmp = value;
    tmp <<= 1;
    if(value < 0.0) tmp = ~tmp;
    IEnumerable<char> result = new List<char>();
    do {
      int v = tmp&0x1f;
      if((tmp>>5) > 0) {
        v |= 0x20;
      }
      v += 63;
      result = result.Append((char)(v));
      tmp = tmp>>5;
    } while(tmp > 0);
    return String.Join("", result);
  }

  public static List<int> ParseInts(String polyline) {
    var iterator = new PolylineIterator(polyline);
    return iterator.ToList(); 
  }

  public static List<Location> ParseGps(String polyline) {
    var iterator = new PolylineIterator(polyline);
    int index = 0;
    int lat = 0, lng = 0;
    IEnumerable<Location> result = new List<Location>();
    foreach(var value in iterator) {
      if(index%2 == 1) {
        lng += value;
        result = result.Append(new Location(){
          Latitude = lat/1E5,
          Longitude = lng/1E5
        });
      } else {
        lat += value;
      }
      index++;
    }
    return result.ToList();
  }
}