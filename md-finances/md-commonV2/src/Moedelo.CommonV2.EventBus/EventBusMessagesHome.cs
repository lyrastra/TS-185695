using Moedelo.CommonV2.EventBus.Home;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<UserWasInOfferEvent> HomeBlUserWasInOffer;
        // ReSharper disable once UnassignedReadonlyField
        /// <summary>
        /// Не испольуется
        /// </summary>
        public static readonly EventBusEventDefinition<PhoneChangedEvent> PhoneChanged;
    }
}