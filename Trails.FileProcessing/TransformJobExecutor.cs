using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Trails.FileProcessing.Models;

namespace Trails.FileProcessing
{
  internal sealed class TransformJobExecutor
  {

    private ITransformChainResolver transformChainResolver;
    private IFileProcessingRepository _repository;
    private ILogger<TransformJobExecutor> _logger;

    public TransformJobExecutor(ILoggerFactory loggerFactory, ITransformChainResolver resolver, IFileProcessingRepository repository) {
      transformChainResolver = resolver;
      _repository = repository;
      _logger = loggerFactory.CreateLogger<TransformJobExecutor>();
    }

    public async Task execute(TransformJob transformJob) {
      if (transformJob == null) throw new ArgumentNullException(nameof(transformJob));
      _logger.LogInformation("Executing jobId={} transform={} fileId={}", transformJob.id, transformJob.transform, transformJob.fileId);
      TransformChain transform = transformChainResolver.resolve(transformJob.transform);
      transformJob.status = FileStatus.PROCESSING;
      transformJob = _repository.SaveTransform(transformJob);
      if (transform == null) {
        throw new ArgumentException("Invalid transform specified in job: " + transformJob.transform);
      }
      try {
        try {
          await transform.transformAsync(transformJob.input, transformJob.context);
        } catch (Exception e) {
          if (e.InnerException != null) throw e.InnerException;
          throw e;
        }
        transformJob.status = FileStatus.DONE;
        transformJob.endTime = DateTime.Now;
      } catch (ArgumentException e) {
        _logger.LogError(e, "ArgumentException Executing jobId={} {}", transformJob.id, e.Message);
        // Set status to errored and update an error message.
        transformJob.status = FileStatus.ERRORED;
        transformJob.endTime = DateTime.Now;
        transformJob.errorMessage = e.Message;
      } catch (Exception e) {
        _logger.LogError(e, "Error Executing jobId={} {}", transformJob.id, e.Message);
        // Set status to errored and update an error message.
        transformJob.status = FileStatus.ERRORED;
        transformJob.endTime = DateTime.Now;
        transformJob.errorMessage = "Something went wrong processing your file.";
      } finally {
        _repository.SaveTransforms(transformJob, transform.getTransformNames());
        transformJob = _repository.SaveTransform(transformJob);
      }
    }
  }
}
