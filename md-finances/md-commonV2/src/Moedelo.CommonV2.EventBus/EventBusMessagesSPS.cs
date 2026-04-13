using Moedelo.CommonV2.EventBus.Sps;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<UserWasInWebinarsEvent> SpsBlUserWasInWebinars;
    }
}