using System;
using Microsoft.EntityFrameworkCore;

[Owned]
public class Location {
  public double Latitude { get; set; }
  public double Longitude { get; set; }

  public double Distance(Location from) {
    // Degrees to Radians
    double DtoR = 0.017453293;
    int R = 6367;

    double rlat1 = this.Latitude * DtoR;
    double rlong1 = this.Longitude * DtoR;
    double rlat2 = from.Latitude * DtoR;
    double rlong2 = from.Longitude * DtoR;

    double dlon = rlong1 - rlong2;
    double dlat = rlat1 - rlat2;

    double chord = Math.Pow(Math.Sin(dlat/2.0), 2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Pow(Math.Sin(dlon/2.0), 2);
    double angle = 2 * Math.Atan2(Math.Sqrt(chord), Math.Sqrt(1-chord));
    double dist = R*angle;

    return dist;
  }

  public static Location operator +(Location a, Location b) => 
    new Location() {
      Latitude = a.Latitude + b.Latitude,
      Longitude = a.Longitude + b.Longitude
    };

  public static Location operator -(Location a, Location b) => 
    new Location() {
      Latitude = a.Latitude - b.Latitude,
      Longitude = a.Longitude - b.Longitude
    };

  public static Location operator *(int by, Location a) => a * by; 

  public static Location operator *(Location a, int by) => 
    new Location() {
      Latitude = a.Latitude*by,
      Longitude = a.Longitude*by
    };

  public static Location operator *(float by, Location a) => a * by;

  public static Location operator *(Location a, float by) => 
    new Location() {
      Latitude = a.Latitude*by,
      Longitude = a.Longitude*by
    };

  public static Location operator /(Location a, float by) => 
    new Location() {
      Latitude = a.Latitude/by,
      Longitude = a.Longitude/by
    };

  public static Location operator /(Location a, double by) => 
    new Location() {
      Latitude = a.Latitude/by,
      Longitude = a.Longitude/by
    };
}
