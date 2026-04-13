using System;
using System.Collections.Concurrent;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions;

namespace Moedelo.Infrastructure.Kafka;

[InjectAsSingleton(typeof(IAdminClientPool))]
internal sealed class AdminClientPool : IAdminClientPool, IDisposable
{
    private readonly ILogger logger;
    private readonly ConcurrentDictionary<string, IAdminClient> pool = new ();
    private bool disposed;

    public AdminClientPool(ILogger<AdminClientPool> logger)
    {
        this.logger = logger;
    }

    public IAdminClient GetAdminClient(string brokerEndpoints)
    {
        CheckDisposed();

        return pool.GetOrAdd(brokerEndpoints, AdminClientFactory);
    }

    private IAdminClient AdminClientFactory(string brokerEndpoints)
    {
        var config = GetConfig(brokerEndpoints);
        return new AdminClientBuilder(config)
            .SetErrorHandler((_, e) =>
            {
                if (e.IsFatal)
                {
                    logger.LogCritical(e.ToString());
                }
                else
                {
                    logger.LogWarning(e.ToString());
                }
            })
            .Build();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        if (disposing)
        {
            foreach (var client in pool.Values)
            {
                client.Dispose();
            }

            pool.Clear();
        }

        disposed = true;
    }

    private void CheckDisposed()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(AdminClientPool));
        }
    }

    private static AdminClientConfig GetConfig(string brokerEndpoints)
    {
        var config = new AdminClientConfig
        {
            BootstrapServers = brokerEndpoints,
            ApiVersionRequest = true,
            SocketKeepaliveEnable = true,
            Acks = Acks.All
        };

        return config;
    }

    ~AdminClientPool()
    {
        Dispose(false);
    }
}
