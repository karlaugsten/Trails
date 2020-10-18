using System.Threading;
using Trails.FileProcessing.Models;

namespace Trails.FileProcessing
{
    public interface ITransformJobQueue
    {
        void enqueue(TransformJob transformJob);
        
        /// <summary>
        /// Blocks until a transformjob becomes available, unless
        /// the CancellationToken is stopped.
        /// </summary>
        /// <returns></returns>
        TransformJob dequeue(CancellationToken stoppingToken);
    }
}
