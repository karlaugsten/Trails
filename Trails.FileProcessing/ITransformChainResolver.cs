
using System.Collections.Generic;

namespace Trails.FileProcessing
{
    public interface ITransformChainResolver {

      TransformChain resolve(string transformName);

      List<string> resolveTransformsForFile(string fileType);
    }
}
