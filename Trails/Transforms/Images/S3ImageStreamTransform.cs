namespace Trails.Transforms {

  public class S3ImageStreamTransform : S3StreamTransform<ImageJobContext>
  {
    public S3ImageStreamTransform(IFileRepository repository) : base(repository) {}
  }
}