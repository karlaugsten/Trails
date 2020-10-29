using System;
using System.Collections.Generic;
using Xunit;
using Trails.FileProcessing;

namespace Trails.Tests
{
  public class TransformBuilderTests
  {
    [Fact]
    public void TestTransformBuilderWithChain()
    {
      MockTransform<string, List<int>, int> transform1 = new Tests.MockTransform<string, List<int>, int>();
      MockTransform<List<int>, List<byte>, int> transform2 = new Tests.MockTransform<List<int>, List<byte>, int>();
      MockTransform<List<byte>, string, int> transform3 = new Tests.MockTransform<List<byte>, string, int>();
      MockEndTransform<string, int> endTransform = new Tests.MockEndTransform<string, int>();

      TransformChainBuilder<int> builder = new TransformChainBuilder<int>();

      builder.loadTransform(transform1)
        .loadTransform(transform2)
        .loadTransform(transform3);

      builder.loadEndTransform(endTransform);

    }

    [Fact]
    public void TestTransformBuilderWithChainPrimitive()
    {
      ITransform<string, int, int> transform1 = new Tests.MockTransform<string, int, int>();
      ITransform<int, byte, int> transform2 = new Tests.MockTransform<int, byte, int>();
      ITransform<byte, string, int> transform3 = new Tests.MockTransform<byte, string, int>();
      IEndTransform<string, int> endTransform = new Tests.MockEndTransform<string, int>();

      TransformChainBuilder<int> builder = new TransformChainBuilder<int>();

      builder.loadTransform(transform1)
        .loadTransform(transform2)
        .loadTransform(transform3);

      builder.loadEndTransform(endTransform);

    }

    [Fact]
    public void TestTransformBuilderWithInvalidChainThrows()
    {
      MockTransform<string, List<int>, int> transform1 = new Tests.MockTransform<string, List<int>, int>();
      MockTransform<List<int>, List<byte>, int> transform2 = new Tests.MockTransform<List<int>, List<byte>, int>();
      MockTransform<List<byte>, string, int> transform3 = new Tests.MockTransform<List<byte>, string, int>();
      MockEndTransform<string, int> endTransform = new Tests.MockEndTransform<string, int>();

      TransformChainBuilder<int> builder = new TransformChainBuilder<int>();

      builder.loadTransform(transform1);

      Assert.Throws<ArgumentException>(() => builder.loadTransform(transform3));
      Assert.Throws<ArgumentException>(() => builder.loadEndTransform(endTransform));

    }
  }
}
