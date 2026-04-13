using System;
using System.Threading.Tasks;
using Moedelo.Infrastructure.RabbitMQ.Abstractions.Models;

namespace Moedelo.Infrastructure.RabbitMQ.Abstractions.Interfaces
{
    public interface IRabbitMqConsumerFactory
    {
        Task ListenAsync<T>(
            ConsumerQueueExchangeConnection exchangeConsumerConnection,
            Func<T, Task> onMessage,
            Func<T, Exception, Task> onException = null,
            ConsumerOptionalConfiguration optionalConfiguration = null) where T : class;
    }
}