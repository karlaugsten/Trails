namespace Trails.Transforms {

  public class S3MapStreamTransform : S3StreamTransform<MapJobContext>
  {
    public S3MapStreamTransform(IFileRepository repository) : base(repository) {}
  }
}