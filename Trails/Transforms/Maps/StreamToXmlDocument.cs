
using System.IO;
using System.Xml;
using Trails.FileProcessing;

namespace Trails.Transforms {

  public class StreamToXmlDocument : ITransform<Stream, XmlDocument, MapJobContext>
  {
    public XmlDocument transform(Stream input, MapJobContext content)
    {
      XmlDocument gpxDoc = new XmlDocument();
      gpxDoc.Load(input);
      return gpxDoc;
    }
  }
}