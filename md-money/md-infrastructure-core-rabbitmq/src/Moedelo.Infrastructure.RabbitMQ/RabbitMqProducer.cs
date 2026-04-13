using System;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.RabbitMQ.Abstractions.Interfaces;
using Moedelo.Infrastructure.RabbitMQ.Abstractions.Models;
using Moedelo.Infrastructure.RabbitMQ.Interfaces;

namespace Moedelo.Infrastructure.RabbitMQ
{
    [InjectAsSingleton(typeof(IRabbitMqProducer))]
    internal sealed class RabbitMqProducer : IRabbitMqProducer
    {
        private readonly IAdvancedBusPool advancedBusPool;

        public RabbitMqProducer(IAdvancedBusPool advancedBusPool)
        {
            this.advancedBusPool = advancedBusPool;
        }

        public async Task ProduceAsync<T>(
            ProducerExchangeConnection connection, 
            string routingKey, 
            T messageBody,
            int? delayMs = null) where T : class
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            
            if (string.IsNullOrWhiteSpace(routingKey))
            {
                throw new ArgumentNullException(nameof(routingKey));
            }

            if (messageBody == null)
            {
                throw new ArgumentNullException(nameof(messageBody));
            }

            var advancedBus = advancedBusPool.GetAdvancedBus(connection.ConnectionString);
            
            var exchange = await advancedBus.ExchangeDeclareAsync(connection.ExchangeName, ExchangeType.Topic, delayed: true)
                .ConfigureAwait(false);
            var message = new Message<T>(messageBody);

            if (delayMs.HasValue)
            {
                message.Properties.Headers.Add("x-delay", delayMs);
            }

            await advancedBus.PublishAsync(exchange, routingKey, true, message).ConfigureAwait(false);
        }
    }
}