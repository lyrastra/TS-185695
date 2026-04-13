using Moedelo.CommonV2.EventBus.Erpt;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<CheckerRegistrationResultsJobStartEvent> CheckerRegistrationResultsJobStartEvent;
        public static readonly EventBusEventDefinition<SendIonAutoRequestJobStartEvent> SendIonAutoRequestJobStartEvent;
        public static readonly EventBusEventDefinition<FundProtocolFileProcessorJobStartEvent> FundProtocolFileProcessorJobStartEvent;
    }
}
