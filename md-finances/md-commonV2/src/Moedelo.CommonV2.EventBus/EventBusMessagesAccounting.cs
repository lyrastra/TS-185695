using Moedelo.CommonV2.EventBus.Accounting;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<IncomingWayBillEvent> CreateWayBill;

        public static readonly EventBusEventDefinition<IncomingWayBillEvent> UpdateWayBill;

        public static readonly EventBusEventDefinition<IncomingWayBillEvent> DeleteWayBill;

    }
}