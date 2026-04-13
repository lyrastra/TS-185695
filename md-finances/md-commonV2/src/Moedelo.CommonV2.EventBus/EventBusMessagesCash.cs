using Moedelo.CommonV2.EventBus.Cash;
using Moedelo.CommonV2.EventBus.Common.MergeProductResult;
using Moedelo.CommonV2.EventBus.Stocks;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
	public partial class EventBusMessages
	{
		public static readonly EventBusEventDefinition<YandexMovementsRequestedEvent> YandexMovementsRequested;

	    public static readonly EventBusEventDefinition<ProductMergeResultEvent> CashEvotorProductMergeResult;

        public static readonly EventBusEventDefinition<EvotorStartUpdateEvent> StartEvotorEntitiesUpdate;
    }
}
