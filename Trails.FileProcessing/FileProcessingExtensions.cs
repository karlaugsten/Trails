using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Trails.FileProcessing
{
    public static class FileProcessingExtensions
    {
        public static void AddFileProcessing(this IServiceCollection services, Action<IFileTransformLoader> configure) {
          services.AddSingleton<ITransformChainResolver>(sp => {
            TransformChainResolver resolver = new TransformChainResolver();
            
            configure(resolver);

            return resolver;
          });

          services.AddScoped<FileProcessor>();
          services.AddScoped<TransformJobExecutor>();
          services.AddHostedService<TransformWorker>();
        }
    }
}
