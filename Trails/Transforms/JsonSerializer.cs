
using System;
using System.IO;
using Newtonsoft.Json;
using Trails.FileProcessing;

namespace Trails.Transforms {

  public class JsonSerializer<TType> : ISerializer<TType>
  {
    public JsonSerializer() {
    }

    public TType deserialize(string str)
    {
      return JsonConvert.DeserializeObject<TType>(str);
    }

    public string serialize(TType obj)
    {
      return JsonConvert.SerializeObject(obj);
    }
  }
}