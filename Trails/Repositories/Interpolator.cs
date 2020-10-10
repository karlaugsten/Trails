using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Interpolator<X, Y> {
  protected X[] x;
  protected Y[] y;


  public Interpolator(X[] x, Y[] y) {
    this.x = x;
    this.y = y;
  }


  /// <summary>
  /// Given the function x,y  return the interpolation at point xprime.
  /// </summary>
  /// <returns></returns>
  public abstract Y interpolate(X xprime);

  public abstract IEnumerable<Y> interpolateAll(X from, X to, X step);
}