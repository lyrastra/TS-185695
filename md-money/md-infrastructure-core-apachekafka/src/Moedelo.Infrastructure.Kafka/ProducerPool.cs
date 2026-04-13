using System;
using System.Collections.Concurrent;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions;

namespace Moedelo.Infrastructure.Kafka
{
    [InjectAsSingleton(typeof(IProducerPool))]
    internal sealed class ProducerPool : IProducerPool, IDisposable
    {
        private readonly ILogger logger;
        private readonly ConcurrentDictionary<string, IProducer<string, string>> pool = new ();
        private bool disposed;

        public ProducerPool(ILogger<ProducerPool> logger)
        {
            this.logger = logger;
        }

        public IProducer<string, string> GetProducer(string brokerEndpoints)
        {
            CheckDisposed();

            var producer = pool.GetOrAdd(brokerEndpoints, ProducerFactory);

            return producer;
        }

        private IProducer<string, string> ProducerFactory(string brokerEndpoints)
        {
            var config = GetConfig(brokerEndpoints);
            var producer = new ProducerBuilder<string, string>(config)
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

            return producer;
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
                foreach (var producer in pool.Values)
                {
                    if (producer != null)
                    {
                        producer.Flush(TimeSpan.FromMilliseconds(100));
                        producer.Dispose();
                    }
                }

                pool.Clear();
            }

            disposed = true;
        }

        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ProducerPool));
            }
        }

        private static ProducerConfig GetConfig(string brokerEndpoints)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = brokerEndpoints,
                ApiVersionRequest = true,
                SocketKeepaliveEnable = true,
                Acks = Acks.Leader,
                MessageTimeoutMs = 10000
            };

            return config;
        }

        ~ProducerPool()
        {
            Dispose(false);
        }
    }
}