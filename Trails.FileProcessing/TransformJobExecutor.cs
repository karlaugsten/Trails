using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Trails.FileProcessing.Models;

namespace Trails.FileProcessing
{
  internal sealed class TransformJobExecutor
  {

    private ITransformChainResolver transformChainResolver;
    private IFileProcessingRepository _repository;

    public TransformJobExecutor(ITransformChainResolver resolver, IFileProcessingRepository repository) {
      transformChainResolver = resolver;
      _repository = repository;
    }

    public async Task execute(TransformJob transformJob) {
      TransformChain transform = transformChainResolver.resolve(transformJob.transform);
      transformJob.status = FileStatus.PROCESSING;
      transformJob = _repository.SaveTransform(transformJob);
      if (transform == null) {
        throw new ArgumentException("Invalid transform specified in job: " + transformJob.transform);
      }
      try {
        await transform.transformAsync(transformJob.input, transformJob.context);
        transformJob.status = FileStatus.DONE;
        _repository.SaveTransforms(transformJob, transform.getTransformNames());
      } catch (Exception e) {
        // Set status to errored and update an error message.
        transformJob.status = FileStatus.ERRORED;
      } finally {
        transformJob = _repository.SaveTransform(transformJob);
      }
    }
  }
}
