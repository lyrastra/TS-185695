using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.InfrastructureV2.EventBus.Internal
{
    public interface ITopicExchangeConsumerFactory : IDI
    {
        Task ListenAsync<T>(
            ExchangeConsumerConnection exchangeConsumerConnection,
            Func<T, Task> onMessage,
            Func<T, Exception, Task> onException = null) where T : class;
    }
}