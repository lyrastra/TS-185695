using System.Threading.Tasks;
using Moedelo.Infrastructure.RabbitMQ.Abstractions.Models;

namespace Moedelo.Infrastructure.RabbitMQ.Abstractions.Interfaces
{
    public interface IRabbitMqProducer
    {
        Task ProduceAsync<T>(ProducerExchangeConnection connection, string routingKey, T messageBody, int? delayMs = null)
            where T : class;
    }
}