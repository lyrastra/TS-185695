using Moedelo.CommonV2.EventBus.Main;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<OutsourcingWasRequestedEvent> OutsourcingWasRequested;
    }
}
