using System;
using System.Threading.Tasks;

namespace Trails.FileProcessing
{
    public interface ISerializer<TOutput>
    {
      string serialize(TOutput obj);
        
      TOutput deserialize(string str);
    }
}
