using System.Collections.Generic;
using System.Linq;

public class LocationInterpolator : Interpolator<double, Location>
{
  private Interpolator<double, double> latitudeInterpolator;
  private Interpolator<double, double> longitudeInterpolator;

  public LocationInterpolator(List<Location> locations) : 
    base(locations.Select(l => l.Latitude).ToArray(), locations.ToArray()) {
      var distances = locations.ToDistances().ToArray();
      latitudeInterpolator = new LinearInterpolator(
        distances,
        locations.Select(l => l.Latitude).ToArray());

      longitudeInterpolator = new LinearInterpolator(
        distances,
        locations.Select(l => l.Longitude).ToArray());
  }

  public override Location interpolate(double xprime) => new Location() {
      Latitude = latitudeInterpolator.interpolate(xprime),
      Longitude = longitudeInterpolator.interpolate(xprime)
    };

  public override IEnumerable<Location> interpolateAll(double from, double to, double step)
  {
    double current = from;
    while (current <= to) {
      yield return interpolate(current);
      current += step;
    }
  }
}