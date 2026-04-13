using System.Collections.Concurrent;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.InfrastructureV2.EventBus.Internal.Pools
{
    [InjectAsSingleton]
    public class TopicExchangePublisherPool : ITopicExchangePublisherPool
    {
        private readonly IAdvancedBusPool advancedBusPool;

        private readonly ConcurrentDictionary<ExchangeConnection, ITopicExchangePublisher> pool =
            new ConcurrentDictionary<ExchangeConnection, ITopicExchangePublisher>(
                new ExchangeConnectionEqualityComparer());

        public TopicExchangePublisherPool(IAdvancedBusPool advancedBusPool)
        {
            this.advancedBusPool = advancedBusPool;
        }

        public ITopicExchangePublisher GetTopicExchangePublisher(ExchangeConnection exchangeConnection)
        {
            var topicExchangePublisher = pool.GetOrAdd(exchangeConnection, TopicExchangePublisherFactory);

            return topicExchangePublisher;
        }

        private ITopicExchangePublisher TopicExchangePublisherFactory(ExchangeConnection exchangeConnection)
        {
            var advancedBus = advancedBusPool.GetAdvancedBus(exchangeConnection.ConnectionString);
            var topicExchangePublisher = new TopicExchangePublisher(exchangeConnection.ExchangeName, advancedBus);

            return topicExchangePublisher;
        }
    }
}