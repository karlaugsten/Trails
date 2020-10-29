
using System.IO;
using Trails.FileProcessing;

namespace Trails.Transforms {

  public class S3StreamTransform<TContext> : ITransform<string, Stream, TContext>
  {
    private IFileRepository _repository;

    public S3StreamTransform(IFileRepository repository) {
      _repository = repository;
    }

    public Stream transform(string input, TContext context)
    {
      return _repository.Get(input);
    }

    public async System.Threading.Tasks.Task<Stream> transformAsync(string input, TContext context)
    {
      return _repository.Get(input);
    }
  }
}