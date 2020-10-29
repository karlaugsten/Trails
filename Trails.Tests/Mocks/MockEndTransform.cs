using System.Threading.Tasks;
using Trails.FileProcessing;

namespace Trails.Tests
{
  public class MockEndTransform<TInput, TContext> : IEndTransform<TInput, TContext>
  {
    public void transform(TInput input, TContext context)
    {
      throw new System.NotImplementedException();
    }

    public Task transformAsync(TInput input, TContext context)
    {
      throw new System.NotImplementedException();
    }
  }
}