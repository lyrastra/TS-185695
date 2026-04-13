using EasyNetQ;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.InfrastructureV2.EventBus.Internal.Pools
{
    public interface IAdvancedBusPool : IDI
    {
        IAdvancedBus GetAdvancedBus(string connectionString);
    }
}