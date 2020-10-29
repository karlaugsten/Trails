using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.Http.BatchFormatters;

namespace Trails
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true)
                .Build();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.WithElasticApmCorrelationInfo()
                .Enrich.WithExceptionDetails()
                .WriteTo.RollingFile("runnify-info-log-{Date}.log")
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
                .CreateLogger();

            Log.Information("Starting Runnify service");

            try {
                CreateWebHostBuilder(args).Build().Run();
            } catch (Exception e) {
                Log.Logger.Error("Error starting service", e);
            }

            Log.CloseAndFlush();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            var elasticHost = "localhost";
            var elasticPort = "9200";
            return new ElasticsearchSinkOptions(new Uri($"http://{elasticHost}:{elasticPort}"))
            {
                AutoRegisterTemplate = true,
                IndexFormat = "{Trails-{0:yyyy.MM.dd}",
                MinimumLogEventLevel = LogEventLevel.Information
            };
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
    }
}
