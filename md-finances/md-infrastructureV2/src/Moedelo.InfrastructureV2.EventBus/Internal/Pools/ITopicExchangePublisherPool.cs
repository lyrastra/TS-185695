using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.InfrastructureV2.EventBus.Internal.Pools
{
    public interface ITopicExchangePublisherPool : IDI
    {
        ITopicExchangePublisher GetTopicExchangePublisher(ExchangeConnection exchangeConnection);
    }
}