using System;

namespace Moedelo.Infrastructure.RabbitMQ.Abstractions.Models
{
    public sealed class ConsumerQueueExchangeConnection
    {
        public ConsumerQueueExchangeConnection(string connectionString, string exchangeName, string queueName, params string[] routingKeyCollection)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (string.IsNullOrWhiteSpace(exchangeName))
            {
                throw new ArgumentNullException(nameof(exchangeName));
            }
            
            if (string.IsNullOrWhiteSpace(queueName))
            {
                throw new ArgumentNullException(nameof(queueName));
            }
            
            if (routingKeyCollection == null || routingKeyCollection.Length == 0)
            {
                throw new ArgumentNullException(nameof(routingKeyCollection));
            }
            
            ConnectionString = connectionString;
            ExchangeName = exchangeName;
            QueueName = queueName;
            RoutingKeyCollection = routingKeyCollection;
        }

        public string ConnectionString { get; }

        public string ExchangeName { get; }
        
        public string QueueName { get; }
        
        public string[] RoutingKeyCollection { get; }
    }
}