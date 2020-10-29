using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trails.FileProcessing
{
    internal class TransformChainResolver : ITransformChainResolver, IFileTransformLoader {

      private Dictionary<string, TransformChain> _transformChains = new Dictionary<string, TransformChain>();
      private Dictionary<string, List<string>> _fileTransforms = new Dictionary<string, List<string>>();

      public TransformChain resolve(string name) {
        return _transformChains[name];
      }

      /// <summary>
      /// Loads the transform chain into the resolver, to be resolved later.
      /// </summary>
      /// <param name="name"></param>
      /// <param name="chain"></param>
    public void loadTransform(string fileType, string name, TransformChain chain) {
      if(_transformChains.ContainsKey(name)) {
        _transformChains[name] = chain;
        loadFileTransform(fileType, name);
        return;
      }
      _transformChains.Add(name, chain);
      loadFileTransform(fileType, name);
    }

    private void loadFileTransform(string fileType, string name) {
      if(_fileTransforms.ContainsKey(fileType)) {
        _fileTransforms[fileType].Add(name);
        return;
      }
      _fileTransforms[fileType] = new List<string>{ name };
    }

    public List<string> resolveTransformsForFile(string fileType) =>
      _fileTransforms[fileType];
  }
}
