using System;

namespace Moedelo.Infrastructure.RabbitMQ.Abstractions.Models
{
    public sealed class ProducerExchangeConnection
    {
        public ProducerExchangeConnection(string connectionString, string exchangeName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (string.IsNullOrWhiteSpace(exchangeName))
            {
                throw new ArgumentNullException(nameof(exchangeName));
            }

            ConnectionString = connectionString;
            ExchangeName = exchangeName;
        }

        public string ConnectionString { get; }

        public string ExchangeName { get; }
    }
}