using System;

namespace Moedelo.InfrastructureV2.EventBus.Internal
{
    public class ExchangeConnection
    {
        public ExchangeConnection(string connectionString, string exchangeName)
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