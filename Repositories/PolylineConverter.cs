using System.Collections.Generic;

/// <summary>
/// Converts a polyline to/from a list of locations.
/// </summary>
public class PolylineConverter {

  public static string Convert(List<Location> points) =>
    points.Select(Convert);

  public static string Convert(Location point) => Convert(point.Latitude) + Convert(point.Longitude);

  public static string Convert(decimal value) {
    unsigned_int tmp = Math.round(value*1E5)<<1;
    if(value < 0.0) tmp = ~tmp;
    string result = "";

    for(int i = 0; i < 6; i++) {
      unsigned_int m = (((0b11111<<(5*i))&tmp)|0x20) + 63;
      result += char(m);
    }
    return result;
  }
}