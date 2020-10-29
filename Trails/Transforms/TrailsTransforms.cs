
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Trails.FileProcessing;

namespace Trails.Transforms {
  /// <summary>
  /// Can be converted to a DB or Config file.
  /// This is meant to store all the transforms applied to an image/file type.
  /// </summary>
  public static class TrailsTransforms
  {
    public static void loadTrailImageTransforms(IServiceProvider services, IFileTransformLoader transformChains) {
      // Loads the TrailImage transforms
      var blurImageTransform = new TransformChainBuilder<ImageJobContext>()
        .loadTransform(services.GetService<S3ImageStreamTransform>())
        .loadTransform(services.GetService<StreamToImageTransform>())
        .loadTransform(services.GetService<ImageResizeAndCropTransform40x25>())
        .loadTransform(services.GetService<ImageToJpegMemStreamTransform20>())
        .loadTransform(services.GetService<StreamToBase64>())
        .loadEndTransform(services.GetService<SaveBlurBase64EndTransform>())
        .loadContextSerializer(new JsonSerializer<ImageJobContext>())
        .build();

      var thumbnailImageTransform = new TransformChainBuilder<ImageJobContext>()
        .loadTransform(services.GetService<S3ImageStreamTransform>())
        .loadTransform(services.GetService<StreamToImageTransform>())
        .loadTransform(services.GetService<ImageResizeAndCropTransform800x500>())
        .loadTransform(services.GetService<ImageToJpegMemStreamTransform90>())
        .loadTransform(services.GetService<SaveJpgImageToS3Transform>())
        .loadEndTransform(services.GetService<SaveThumbnailImageEndTransform>())
        .loadContextSerializer(new JsonSerializer<ImageJobContext>())
        .build();

      var mainImageTransform = new TransformChainBuilder<ImageJobContext>()
        .loadTransform(services.GetService<S3ImageStreamTransform>())
        .loadTransform(services.GetService<StreamToImageTransform>())
        .loadTransform(services.GetService<ImagePreserveAspectResizeTransform>())
        .loadTransform(services.GetService<ImageToJpegMemStreamTransform94>())
        .loadTransform(services.GetService<SaveJpgImageToS3Transform>())
        .loadEndTransform(services.GetService<SaveMainImageEndTransform>())
        .loadContextSerializer(new JsonSerializer<ImageJobContext>())
        .build();

      transformChains.loadTransform("TrailImage", "BlurImageTransform", blurImageTransform);
      transformChains.loadTransform("TrailImage", "ThumbnailImageTransform", thumbnailImageTransform);
      transformChains.loadTransform("TrailImage", "MainImageTransform", mainImageTransform);
    }

    public static void loadGpxFileTransforms(IServiceProvider services, IFileTransformLoader transformChains) {
      // Loads the GpxFile transforms
      var elevationPolylineTransform = new TransformChainBuilder<MapJobContext>()
        .loadTransform(services.GetService<S3MapStreamTransform>())
        .loadTransform(services.GetService<StreamToXmlDocument>())
        .loadTransform(services.GetService<XmlDocumentElevation20MeterStep>())
        .loadTransform(services.GetService<ElevationPolylineConverter>())
        .loadEndTransform(services.GetService<ElevationPolylinePopulator>())
        .loadContextSerializer(new JsonSerializer<MapJobContext>())
        .build();

      var locationPolylineTransform = new TransformChainBuilder<MapJobContext>()
        .loadTransform(services.GetService<S3MapStreamTransform>())
        .loadTransform(services.GetService<StreamToXmlDocument>())
        .loadTransform(services.GetService<XmlDocumentToLocations>())
        .loadTransform(services.GetService<LocationInterpolator50MeterStep>())
        .loadTransform(services.GetService<LocationPolylineConverter>())
        .loadEndTransform(services.GetService<LocationPolylinePopulator>())
        .loadContextSerializer(new JsonSerializer<MapJobContext>())
        .build();

      var startEndLocationTransform = new TransformChainBuilder<MapJobContext>()
        .loadTransform(services.GetService<S3MapStreamTransform>())
        .loadTransform(services.GetService<StreamToXmlDocument>())
        .loadTransform(services.GetService<XmlDocumentToLocations>())
        .loadEndTransform(services.GetService<StartEndLocationPopulator>())
        .loadContextSerializer(new JsonSerializer<MapJobContext>())
        .build();

      transformChains.loadTransform("MapFile", "ElevationPolylineTransform", elevationPolylineTransform);
      transformChains.loadTransform("MapFile", "LocationPolylineTransform", locationPolylineTransform);
      transformChains.loadTransform("MapFile", "StartEndLocationTransform", startEndLocationTransform);
    }

    public static void loadGpxFileTransforms() {
      // Loads the Gpx file related transforms
    }
  }
}