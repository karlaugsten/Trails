
using System.Collections.Generic;

namespace Trails.FileProcessing
{
    public interface IFileTransformLoader {

      /// <summary>
      /// Loads the transform chain into the resolver, to be resolved later.
      /// </summary>
      /// <param name="name"></param>
      /// <param name="chain"></param>
      void loadTransform(string fileType, string name, TransformChain chain);
    }
}
