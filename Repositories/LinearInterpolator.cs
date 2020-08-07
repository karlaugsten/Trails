

using System;

public class LinearInterpolator : Interpolator
{
  public LinearInterpolator(double[] x, double[] y) : base(x, y)
  {
  }

  public override double interpolate(double xprime)
  {
    // Binary search the two points where x1 > xprime > x2

    int xi = this.BinarySearch(this.x, xprime);
    // Value is somewhere between xi and xi + 1, 
    // return the linear interpolation between points at xi and xi+1 at xprime
    if(Math.Abs(this.x[xi+1] - this.x[xi]) < 1E-8) return (this.y[xi] + this.y[xi+1])/2.0;

    return (this.y[xi] * (this.x[xi + 1] - xprime) + this.y[xi + 1] * (xprime - this.x[xi]))/(this.x[xi + 1] - this.x[xi]);
  }

  public int BinarySearch(double[] a, double item)
  {
      int first = 0;
      int last = a.Length - 1;
      int mid = 0;
      do
      {
          mid = first + (last - first) / 2;
          if (item.CompareTo(a[mid]) > 0)
              first = mid + 1;
          else
              last = mid - 1;
          if (a[mid].CompareTo(item) == 0)
              return mid;
      } while (first <= last);
      return mid;
  }
}