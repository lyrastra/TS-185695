using Moedelo.CommonV2.EventBus.IqDialer;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<IqDialerCallEndedEvent> IqDialerCallEndedEvent;
        public static readonly EventBusEventDefinition<IqDialerLeadsProcessedEvent> IqDialerLeadsProcessed;
    }
}