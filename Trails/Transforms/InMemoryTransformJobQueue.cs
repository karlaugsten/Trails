
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using Trails.FileProcessing;
using Trails.FileProcessing.Models;

namespace Trails.Transforms {

  public class InMemoryTransformJobQueue : ITransformJobQueue
  {
    private ConcurrentQueue<TransformJob> _queue;
    private ManualResetEvent waiter = new ManualResetEvent(false);

    public InMemoryTransformJobQueue() {
      _queue = new ConcurrentQueue<TransformJob>();
    }

    public TransformJob dequeue(CancellationToken stoppingToken)
    {
      TransformJob job;
      if(_queue.IsEmpty) {
        waiter.WaitOne();
      }
      var result = _queue.TryDequeue(out job);
      if(result) return job;
      return null;
    }

    public void enqueue(TransformJob transformJob)
    {
      _queue.Enqueue(transformJob);
      waiter.Set();
    }
  }
}