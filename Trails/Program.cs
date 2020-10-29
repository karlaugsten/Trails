using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Apm.SerilogEnricher;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Http.BatchFormatters;

namespace Trails
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var elasticHost = "localhost";
            var elasticPort = "9200";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.WithElasticApmCorrelationInfo()
                .Enrich.WithExceptionDetails()
                .WriteTo.RollingFile("runnify-info-log-{Date}.log")
                .WriteTo.DurableHttpUsingFileSizeRolledBuffers(
                    requestUri: new Uri($"http://{elasticHost}:{elasticPort}").ToString(),
                    batchFormatter: new ArrayBatchFormatter(),
                    textFormatter: new ElasticsearchJsonFormatter())
                .CreateLogger();

            Log.Information("Starting Runnify service");

            try {
                CreateWebHostBuilder(args).Build().Run();
            } catch (Exception e) {
                Log.Logger.Error("Error starting service", e);
            }

            Log.CloseAndFlush();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
    }
}
