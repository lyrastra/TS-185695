using Moedelo.CommonV2.EventBus.TimeDigital;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<SyncTimeDigitalContactCommand> SyncTimeDigitalContact;
    }
}