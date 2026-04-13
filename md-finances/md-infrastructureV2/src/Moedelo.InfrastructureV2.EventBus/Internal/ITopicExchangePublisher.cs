using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.EventBus.Internal
{
    public interface ITopicExchangePublisher
    {
        Task PublishAsync<T>(string routingKey, T messageBody, int? delayMs = null) where T : class;
    }
}