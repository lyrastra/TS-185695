using System.Diagnostics;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Hosting;
using Moedelo.Common.Consul.ServiceDiscovery.Abstractions;

namespace Moedelo.Common.Consul.ServiceDiscovery.Internals;

internal sealed class MoedeloConsulServiceDiscoveryHostedService : BackgroundService
{
    private readonly IMoedeloConsulServiceDiscovery consulServiceDiscovery;
    private readonly IServer server;
    private readonly TaskCompletionSource<bool> applicationStarted = new();
    private readonly CancellationTokenRegistration lifeTimeRegistration;

    public MoedeloConsulServiceDiscoveryHostedService(
        IMoedeloConsulServiceDiscovery consulServiceDiscovery,
        IHostApplicationLifetime appLifetime,
        IServer server)
    {
        this.consulServiceDiscovery = consulServiceDiscovery;
        this.server = server;

        this.lifeTimeRegistration = appLifetime.ApplicationStarted.Register(() =>
        {
            applicationStarted.TrySetResult(true);
        });
    }

    public override void Dispose()
    {
        lifeTimeRegistration.Dispose();
            
        base.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var listenerAddress = await GetListenerAddressAsync(stoppingToken).ConfigureAwait(false);

        await consulServiceDiscovery.ExecuteAsync(listenerAddress, stoppingToken).ConfigureAwait(false);
    }

    private async Task<Uri> GetListenerAddressAsync(CancellationToken cancellationToken)
    {
        await WaitForApplicationStartedAsync(cancellationToken).ConfigureAwait(false);
        Debug.Assert(applicationStarted.Task.IsCompleted);

        var listenerAddress = server.Features.Get<IServerAddressesFeature>().Addresses.FirstOrDefault();

        if (listenerAddress == null)
        {
            throw new NullReferenceException("Регистрация в консуле невозможна: невозможно получить listen address");
        }

        return new Uri(listenerAddress);
    }

    private Task<bool> WaitForApplicationStartedAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => applicationStarted.Task, stoppingToken);
    }
}
