
using System;
using System.IO;
using Trails.FileProcessing;

namespace Trails.Transforms {

  public class StreamToBase64 : ITransform<Stream, string, ImageJobContext>
  {
    public StreamToBase64() {
    }

    public string transform(Stream input, ImageJobContext context)
    {
      byte[] buffer = new byte[input.Length];
      input.Position = 0;
      input.Read(buffer, 0, (int)input.Length);
      var output = Convert.ToBase64String(buffer);
      input.Dispose();
      return output;
    }

    public async System.Threading.Tasks.Task<string> transformAsync(Stream input, ImageJobContext context)
    {
      byte[] buffer = new byte[input.Length];
      input.Position = 0;
      input.Read(buffer, 0, (int)input.Length);
      var output = Convert.ToBase64String(buffer);
      await input.DisposeAsync();
      return output;
    }
  }
}