namespace Moedelo.InfrastructureV2.EventBus
{
    public interface IEventBusConfigurator
    {
        string GetNamePrefixConfig();
        string GetClientId();
        string GetEventBusConfig();
    }
}
