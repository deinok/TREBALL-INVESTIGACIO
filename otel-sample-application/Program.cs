using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OtelSampleApplication;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
            .AddOpenTelemetry()
            .WithMetrics(meterProviderBuilder =>
            {
                meterProviderBuilder
                    .AddRuntimeInstrumentation();
            })
            .WithTracing(tracerProviderBuilder =>
            {
            })
            .UseOtlpExporter();
        services
            .AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
