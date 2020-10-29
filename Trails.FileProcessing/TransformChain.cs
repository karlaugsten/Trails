using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;

namespace Trails.FileProcessing
{
  /// <summary>
  /// Transforms from TInput to TOutput through a series of transforms.
  /// </summary>
  public class TransformChain
  {
    private ArrayList _transforms;
    private object _endTransform;

    private object _contextSerializer;

    internal TransformChain(ArrayList transforms, object endTransform, object contextSerializer) {
      _endTransform = endTransform;
      _transforms = transforms;
      _contextSerializer = contextSerializer;
    }

    public void transform(object input, string contextString) {
      object output = input;
      object context = getDeserializedContext(contextString);
      foreach(var transform in _transforms) {
        var transformType = transform.GetType();
        MethodInfo transformMethod = transformType.GetMethods().First(m => m.Name == "transform");
        output = transformMethod.Invoke(transform, new [] {output, context});
      }
      var endTransformType = _endTransform.GetType();
      MethodInfo endTransformMethod = endTransformType.GetMethods().First(m => m.Name == "transform");
      endTransformMethod.Invoke(_endTransform, new [] {output, context});
    }

    public async Task transformAsync(object input, string contextString) {
      object output = input;
      object context = getDeserializedContext(contextString);
      foreach(var transform in _transforms) {
        var transformType = transform.GetType();
        MethodInfo transformMethod = transformType.GetMethods().First(m => m.Name == "transform");
        output = transformMethod.Invoke(transform, new [] {output, context});
      }
      var endTransformType = _endTransform.GetType();
      MethodInfo endTransformMethod = endTransformType.GetMethods().First(m => m.Name == "transform");
      endTransformMethod.Invoke(_endTransform, new [] {output, context});
    }

    private object getDeserializedContext(string contextString) {
      var serializerType = _contextSerializer.GetType();
      MethodInfo deserialize = serializerType.GetMethods().First(m => m.Name == "deserialize");
      return deserialize.Invoke(_contextSerializer, new [] {contextString});
    }

    /// <summary>
    /// Returns the list of transform names in this transform chain.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> getTransformNames() {
      foreach(var transform in _transforms) {
        yield return transform.GetType().Name; 
      }
      yield return _endTransform.GetType().Name;
    }
  }
}
