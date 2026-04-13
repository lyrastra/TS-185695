using Moedelo.CommonV2.EventBus.Erpt;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<NeformalDocumentsLoadManualyEvent> NeformalDocumentsLoadManualy;
        
        public static readonly EventBusEventDefinition<NeformalDocumentsLoadAutomaticallyEvent> NeformalDocumentsLoadAutomatically;

        public static readonly EventBusEventDefinition<NeformalDocumentsIonLoadAutomaticallyEvent> NeformalDocumentsIonLoadAutomatically;
    }
}