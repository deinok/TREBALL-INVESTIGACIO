using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OtelSampleApplication;

public class Worker(ILogger<Worker> logger) : IHostedService
{

    private readonly ILogger<Worker> logger = logger;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Timed Hosted Service running.");
        using var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        while (await periodicTimer.WaitForNextTickAsync(cancellationToken))
        {
            this.logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

}