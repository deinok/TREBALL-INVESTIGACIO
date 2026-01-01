using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OtelSampleApplication;
using System;

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
            .UseOtlpExporter(OtlpExportProtocol.Grpc, new Uri("http://localhost:63174"));
        services
            .AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
