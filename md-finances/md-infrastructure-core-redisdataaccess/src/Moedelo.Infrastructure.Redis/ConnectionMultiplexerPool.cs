using System;
using System.Collections.Concurrent;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Redis.Abstractions.Models;
using StackExchange.Redis;

namespace Moedelo.Infrastructure.Redis
{
    [InjectAsSingleton(typeof(IConnectionMultiplexerPool))]
    internal sealed class ConnectionMultiplexerPool : IConnectionMultiplexerPool, IDisposable
    {
        private ConcurrentDictionary<IRedisConnection, ConnectionMultiplexer> pool = 
            new (new RedisConnectionEqualityComparer());

        private bool disposed;

        public ConnectionMultiplexer GetConnectionMultiplexer(IRedisConnection connection)
        {
            CheckDisposed();

            var connectionMultiplexer = pool.GetOrAdd(connection, ConnectionMultiplexerFactory);

            return connectionMultiplexer;
        }

        private static ConnectionMultiplexer ConnectionMultiplexerFactory(IRedisConnection connection)
        {
            var connectionMultiplexer = string.IsNullOrWhiteSpace(connection.ConnectionString)
                ? null
                : ConnectionMultiplexer.Connect(connection.ConnectionString);

            return connectionMultiplexer;
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
                foreach (var connectionMultiplexer in pool.Values)
                {
                    connectionMultiplexer?.Dispose();
                }

                pool = null;
            }

            disposed = true;
        }

        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ConnectionMultiplexerPool));
            }
        }

        ~ConnectionMultiplexerPool()
        {
            Dispose(false);
        }
    }
}