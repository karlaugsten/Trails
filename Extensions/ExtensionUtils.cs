using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

public static class ExtensionUtils {
  /// <summary>
  /// Converts the list of gps locations into the distances from the first location.
  /// </summary>
  /// <param name="locations"></param>
  /// <returns></returns>
  public static IEnumerable<double> ToDistances(this List<Location> locations) =>
    locations.ToImmutableArray()
    .Aggregate<IEnumerable<Tuple<double, Location>>, IEnumerable<double>, Location>(
      new List<Tuple<double, Location>>() { Tuple.Create(0.0, locations.First()) },
      (accum, location) => accum.Append(Tuple.Create(accum.Last().Item1 + location.Distance(accum.Last().Item2), location)),
      (accum) => accum.Select(tuple => tuple.Item1)
     );

  /// <summary>
  /// Converts the list of gps locations into the total distance.
  /// </summary>
  /// <param name="locations"></param>
  /// <returns></returns>
  public static double ToTotalDistance(this List<Location> locations) =>
    locations.ToImmutableArray()
    .Aggregate<Tuple<double, Location>, double, Location>(
      Tuple.Create(0.0, locations.First()),
      (accum, location) => Tuple.Create(accum.Item1 + location.Distance(accum.Item2), location),
      (tuple) => tuple.Item1
     );
}