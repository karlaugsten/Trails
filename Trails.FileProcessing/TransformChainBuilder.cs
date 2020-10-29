using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trails.FileProcessing
{
  /// <summary>
  /// Loads a chain of transforms and verifies that it is a valid transformer chain.
  /// </summary>
  public class TransformChainBuilder<TContext>
  {
    private ArrayList _transforms;
    private object _endTransform;

    private ISerializer<TContext> _contextSerializer;
    
    public TransformChainBuilder() {
      _transforms = new ArrayList();
    }

    public TransformChainBuilder<TContext> loadTransform<TInput, TOutput>(ITransform<TInput, TOutput, TContext> transform) {
      // TODO: Validate that the context is the same for all transforms.
      if(_transforms.Count == 0) {
        _transforms.Add(transform);
        return this;
      } 
      
      object last = _transforms[_transforms.Count - 1];
      // Verify that the output of the last argument matches the input
      // of the next transform.
      if (GetInputTransformType(transform) != GetOutputTransformType(last)) {
        throw new ArgumentException($"Invalid transform: Input type {GetInputTransformType(transform)} must match the previously loaded output type {GetOutputTransformType(last)}");
      }
      _transforms.Add(transform);
      return this;
    }

    public TransformChainBuilder<TContext> loadEndTransform<TInput>(IEndTransform<TInput, TContext> endTransform) {
      // TODO: Validate that the context is the same for all transforms.
      object last = _transforms[_transforms.Count - 1];
      // Verify that the output of the last argument matches the input
      // of the next transform.
      if (GetEndTransformType(endTransform) != GetOutputTransformType(last)) {
        throw new ArgumentException($"Invalid transform: Input type {GetEndTransformType(endTransform)} must match the previously loaded output type {GetOutputTransformType(last)}");
      }
      _endTransform = endTransform;
      return this;
    }

    public TransformChainBuilder<TContext> loadContextSerializer(ISerializer<TContext> serializer) {
      this._contextSerializer = serializer;
      return this;
    }

    public TransformChain build() {
      if (_endTransform == null) {
        throw new ArgumentException("TransformChainBuilder requires an end transform to be loaded.");
      }
      if (_contextSerializer == null) {
        throw new ArgumentException("TransformChainBuilder requires a contextSerializer to be loaded.");
      }
      return new TransformChain(_transforms, _endTransform, _contextSerializer);
    }


    private Type GetOutputTransformType(object transform) => 
      GetTransformType(transform, typeof(ITransform<,,>), 1);

    private Type GetInputTransformType(object transform) =>
      GetTransformType(transform, typeof(ITransform<,,>), 0);

    private Type GetEndTransformType(object endTransform) =>
      GetTransformType(endTransform, typeof(IEndTransform<,>), 0);

    private Type GetTransformType<TType>(TType transform, Type type, int pos) {
      List<Type> genTypes = new List<Type>();
      foreach(var intType in transform.GetType().GetInterfaces()) {
        if(intType.IsGenericType && intType.GetGenericTypeDefinition()
            == type) {
            return intType.GetGenericArguments()[pos];
        }
      }
      throw new ArgumentException("Invalid transform argument");
    }
  }
}
