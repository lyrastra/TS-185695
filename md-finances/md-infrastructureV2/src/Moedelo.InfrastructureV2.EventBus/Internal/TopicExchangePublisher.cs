using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;

namespace Moedelo.InfrastructureV2.EventBus.Internal
{
    internal sealed class TopicExchangePublisher : ITopicExchangePublisher
    {
        private readonly string exchangeName;

        private readonly IAdvancedBus advancedBus;

        public TopicExchangePublisher(string exchangeName, IAdvancedBus advancedBus)
        {
            this.exchangeName = exchangeName;
            this.advancedBus = advancedBus;
        }

        public async Task PublishAsync<T>(string routingKey, T messageBody, int? delayMs = null) where T : class
        {
            var exchange = await advancedBus.ExchangeDeclareAsync(exchangeName, ExchangeType.Topic, delayed: true)
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