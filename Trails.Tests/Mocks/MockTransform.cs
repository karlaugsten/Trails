using System.Threading.Tasks;
using Trails.FileProcessing;

namespace Trails.Tests
{
  public class MockTransform<TInput, TOutput, TContext> : ITransform<TInput, TOutput, TContext>
  {
    public TOutput transform(TInput input, TContext context)
    {
      throw new System.NotImplementedException();
    }

    public Task<TOutput> transformAsync(TInput input, TContext context)
    {
      throw new System.NotImplementedException();
    }
  }
}