using System;

public abstract class Interpolator {
  protected double[] x;
  protected double[] y;


  public Interpolator(double[] x, double[] y) {
    this.x = x;
    this.y = y;
  }


  /// <summary>
  /// Given the function x,y  return the interpolation at point xprime.
  /// </summary>
  /// <returns></returns>
  public abstract double interpolate(double xprime);
}