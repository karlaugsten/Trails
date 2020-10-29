using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Elastic.Apm.SerilogEnricher;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.Http.BatchFormatters;

namespace Trails
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithElasticApmCorrelationInfo()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("Environment", environment)  
                .WriteTo.RollingFile("runnify-info-log-{Date}.log")
                .WriteTo.Elasticsearch(ConfigureElasticSink(environment))
                .CreateLogger();

            Log.Information("Starting Runnify service");

            try {
                CreateWebHostBuilder(args).Build().Run();
            } catch (Exception e) {
                Log.Error(e, "Error starting service");
            }

            Log.CloseAndFlush();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(string environment)
        {
            var elasticHost = "localhost";
            var elasticPort = "9200";
            return new ElasticsearchSinkOptions(new Uri($"http://{elasticHost}:{elasticPort}"))
            {
                AutoRegisterTemplate = true,
                IndexFormat = "Trails-{0:yyyy.MM.dd}",
                MinimumLogEventLevel = LogEventLevel.Information,
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog
            };
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
    }
}
