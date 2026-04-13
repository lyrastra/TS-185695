using System;

namespace Moedelo.InfrastructureV2.EventBus.Internal
{
    public class ExchangeConsumerConnection : ExchangeConnection
    {
        public ExchangeConsumerConnection(string connectionString, string exchangeName, string queueName, ushort prefetchCount = 50, params string[] routingKeyCollection)
            : base(connectionString, exchangeName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
            {
                throw new ArgumentNullException(nameof(queueName));
            }
            
            if (routingKeyCollection == null || routingKeyCollection.Length == 0)
            {
                throw new ArgumentNullException(nameof(routingKeyCollection));
            }
            
            QueueName = queueName;
            PrefetchCount = prefetchCount;
            RoutingKeyCollection = routingKeyCollection;
        }

        public ExchangeConsumerConnection(string connectionString, string exchangeName, string queueName, ushort prefetchCount = 50, bool skipRedelivered = false, params string[] routingKeyCollection)
            : base(connectionString, exchangeName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
            {
                throw new ArgumentNullException(nameof(queueName));
            }

            if (routingKeyCollection == null || routingKeyCollection.Length == 0)
            {
                throw new ArgumentNullException(nameof(routingKeyCollection));
            }

            QueueName = queueName;
            PrefetchCount = prefetchCount;
            RoutingKeyCollection = routingKeyCollection;
            SkipRedelivered = skipRedelivered;
        }

        public string QueueName { get; }
        
        public string[] RoutingKeyCollection { get; }

        public ushort PrefetchCount { get; }

        public bool SkipRedelivered { get; private set; }
    }
}