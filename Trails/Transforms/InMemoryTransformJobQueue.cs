
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using Trails.FileProcessing;
using Trails.FileProcessing.Models;

namespace Trails.Transforms {

  public class InMemoryTransformJobQueue : ITransformJobQueue
  {
    private ConcurrentQueue<TransformJob> _queue;
    private ManualResetEventSlim waiter = new ManualResetEventSlim(false);

    public InMemoryTransformJobQueue() {
      _queue = new ConcurrentQueue<TransformJob>();
      waiter.Reset();
    }

    public TransformJob dequeue(CancellationToken stoppingToken)
    {
      TransformJob job;
      if(_queue.IsEmpty) {
        waiter.Wait(stoppingToken);
      }
      var result = _queue.TryDequeue(out job);
      if (result) return job;
      waiter.Reset();
      return null;
    }

    public void enqueue(TransformJob transformJob)
    {
      bool empty = _queue.IsEmpty;
      _queue.Enqueue(transformJob);
      waiter.Set();
    }
  }
}