using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

public interface IPublisherFactory : IDI
{
    IPublisher<T> GetForAllClient<T>(EventBusEventDefinition<T> definition) where T : class;
}