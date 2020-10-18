using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Trails.FileProcessing.Models;

namespace Trails.FileProcessing
{
  /// <summary>
  /// The main class that consumers of the file processing library will interact with.
  /// </summary>
  public class FileProcessor
  {
    private IFileProcessingRepository _fileRepository;
    private ITransformChainResolver _transformResolver;

    private ITransformJobQueue _queue;

    private ILogger<FileProcessor> _logger;

    private ILoggerFactory _loggerFactory;

    public FileProcessor(IFileProcessingRepository fileRepository, ILoggerFactory loggerFactory, ITransformChainResolver resolver, ITransformJobQueue queue) {
      _fileRepository = fileRepository;
      _queue = queue;
      _logger = loggerFactory.CreateLogger<FileProcessor>();
      _loggerFactory = loggerFactory;
      _transformResolver = resolver;
    }

    public async Task<FileTransform> process<TContext>(string fileType, string input, TContext context) {
      // TODO: Create a file to track the progress of the transform and whatnot.
      var file = new FileTransform() {
        context = JsonConvert.SerializeObject(context),
        s3Location = input,
        fileType = fileType,
        status = FileStatus.QUEUED
      };

      file = _fileRepository.SaveFile(file);
      
      var transforms = _transformResolver.resolveTransformsForFile(fileType);
      transforms.ForEach(t => queueTransform(file, t));
      return file;
    }

    /// <summary>
    /// Iterates through all existing files, and checks if the transforms have been applied to 
    /// the existing file. If not, it will re-apply the transforms.
    /// </summary>
    public void ApplyTransforms() {
      foreach(var file in _fileRepository.GetFiles()) {
        var appliedTransforms = _fileRepository.GetAppliedTransforms(file.id);
        var currentTransformsForFile = _transformResolver.resolveTransformsForFile(file.fileType);
        foreach(var curTransform in currentTransformsForFile) {
          TransformChain chain = _transformResolver.resolve(curTransform);
          var currentTransforms = chain.getTransformNames();
          var appliedTransformChain = appliedTransforms[curTransform];
          if(!appliedTransforms.ContainsKey(curTransform) || !currentTransforms.SequenceEqual(appliedTransformChain)) {
            // Add a job for the current transform
            queueTransform(file, curTransform);
          }
        }
      }
    }

    /// <summary>
    /// TODO: Move this to a class "TransformJobQueue" that handles the queueing of
    /// transform jobs so that this can be abstracted to a worker process.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="transform"></param>
    /// <typeparam name="TContext"></typeparam>
    private void queueTransform(FileTransform file, string transform) {
      _queue.enqueue(new TransformJob() {
        transform = transform,
        status = FileStatus.QUEUED,
        startTime = DateTime.Now,
        input = file.s3Location,
        context = file.context,
        fileId = file.id
      });
    }

  }
}
