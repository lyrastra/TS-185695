using Moedelo.CommonV2.EventBus.Bookkeeping;
using Moedelo.CommonV2.EventBus.Common.MergeProductResult;
using Moedelo.CommonV2.EventBus.Stocks;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<AccountingPolicyChangedEvent> BookkeepingAccountingPolicyChanged;

        public static readonly EventBusEventDefinition<InventoryCardCreatedEvent> BookkeepingInventoryCardCreated;
        
        public static readonly EventBusEventDefinition<InventoryCardDeletedEvent> BookkeepingInventoryCardDeleted;

        public static readonly EventBusEventDefinition<InvoiceCreatedEvent> BookkeepingInvoiceCreated;
            
        public static readonly EventBusEventDefinition<InvoiceDeletedEvent> BookkeepingInvoiceDeleted;

        public static readonly EventBusEventDefinition<InvoiceUpdatedEvent> BookkeepingInvoiceUpdated;

        public static readonly EventBusEventDefinition<MoneyBalanceCompletedEvent> BookkeepingMoneyBalanceCompleted;

        public static readonly EventBusEventDefinition<PeriodClosedEvent> BookkeepingPeriodClosed;

        public static readonly EventBusEventDefinition<PeriodOpenedEvent> BookkeepingPeriodOpened;
        
        public static readonly EventBusEventDefinition<AutoClosePeriodCompletedEvent> BookkeepingAutoClosePeriodCompleted;

        public static readonly EventBusEventDefinition<AccountingBalancesChangedEvent> BookkeepingAccountingBalancesChanged;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> BookkeepingRetailReportMergeResult;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> BookkeepingPurchaseWaybillMergeResult;

		public static readonly EventBusEventDefinition<ProductMergeResultEvent> BookkeepingSaleWaybillMergeResult;
    }
}