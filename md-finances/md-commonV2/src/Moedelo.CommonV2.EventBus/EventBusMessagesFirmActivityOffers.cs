using Moedelo.CommonV2.EventBus.FirmActivityOffers;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<FirmActivityOffersTriggerFiredEvent> FirmActivityOffersTriggerFired;
    }
}