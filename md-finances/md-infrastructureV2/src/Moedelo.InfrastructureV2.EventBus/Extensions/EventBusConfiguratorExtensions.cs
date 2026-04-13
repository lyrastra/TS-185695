using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.InfrastructureV2.EventBus.Extensions;

internal static class EventBusConfiguratorExtensions
{
    internal static string GetExchangeName<T>(this IEventBusConfigurator eventBusConfigurator,
        EventBusEventDefinition<T> definition)
        where T : class
    {
        return $"{eventBusConfigurator.GetNamePrefixConfig()}.MD.{definition.ExchangeName}.V2.E";
    }

    internal static string GetQueueName<T>(this IEventBusConfigurator eventBusConfigurator,
        EventBusEventDefinition<T> definition)
        where T : class
    {
        return $"{eventBusConfigurator.GetNamePrefixConfig()}.MD.{definition.ExchangeName}.V2.Q.{eventBusConfigurator.GetClientId()}";
    }

    internal static string GetClientRoutingKey(this IEventBusConfigurator eventBusConfigurator)
    {
        return $"ClientId.{eventBusConfigurator.GetClientId()}";
    }
}
