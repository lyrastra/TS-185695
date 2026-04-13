using System;
using System.Collections.Concurrent;
using Confluent.Kafka;
using Moedelo.InfrastructureV2.ApacheKafka.Abstractions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.InfrastructureV2.ApacheKafka.Implementations
{
    [InjectAsSingleton(typeof(IProducerPool))]
    public sealed class ProducerPool : IProducerPool, IDisposable
    {
        private const string Tag = nameof(ProducerPool);
        private ConcurrentDictionary<string, IProducer<string, string>> pool =
            new ConcurrentDictionary<string, IProducer<string, string>>();

        private readonly ILogger logger;
        private bool disposed;

        public ProducerPool(ILogger logger)
        {
            this.logger = logger;
        }

        public IProducer<string, string> GetProducer(string brokerEndpoints)
        {
            CheckDisposed();

            var producer = pool.GetOrAdd(brokerEndpoints, ProducerFactory);

            return producer;
        }
        
        private static IProducer<string, string> ProducerFactory(string brokerEndpoints)
        {
            return new ProducerBuilder<string, string>(GetConfig(brokerEndpoints)).Build();
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
                logger.Debug(Tag, $"Disposing producers pull ({pool.Count})");

                foreach (var producer in pool.Values)
                {                    
                    producer?.Dispose();
                }

                pool.Clear();

                pool = null;
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