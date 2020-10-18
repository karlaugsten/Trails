using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Trails.FileProcessing {
  internal sealed class TransformWorker : BackgroundService
  {
      private readonly ILogger<TransformWorker> _logger;
      private readonly ITransformJobQueue _queue;

      private readonly IServiceScopeFactory _serviceFactory;

      public TransformWorker(ITransformJobQueue queue, 
          ILoggerFactory loggerFactory, IServiceScopeFactory serviceFactory)
      {
          _logger = loggerFactory.CreateLogger<TransformWorker>();
          _queue = queue;
          _serviceFactory = serviceFactory;
      }

      protected override Task ExecuteAsync(CancellationToken stoppingToken)
      {
        _logger.LogInformation(
              $"TransformWorker hosted service is running");

        return Task.Run(() => BackgroundProcessing(stoppingToken));
      }

      private async Task BackgroundProcessing(CancellationToken stoppingToken)
      {
          while (!stoppingToken.IsCancellationRequested)
          {
              var transformJob = 
                  _queue.dequeue(stoppingToken);

              try
              {
                using (var scope = _serviceFactory.CreateScope())
                {
                  var executor = scope.ServiceProvider.GetRequiredService<TransformJobExecutor>();
                  await executor.execute(transformJob);
                }
              }
              catch (Exception ex)
              {
                  _logger.LogError(ex, 
                      "Error occurred executing transform job with transform {}.", transformJob.transform);
              }
          }
      }

      public override async Task StopAsync(CancellationToken stoppingToken)
      {
          _logger.LogInformation("Queued Hosted Service is stopping.");

          await base.StopAsync(stoppingToken);
      }
  }
}