using Moedelo.CommonV2.EventBus.Common.MergeProductResult;
using Moedelo.CommonV2.EventBus.Stocks;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        public static readonly EventBusEventDefinition<ChangeProductSubtypeEvent> ChangeProductSubtype;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeToRetailReportApiCommand;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeToPurchaseWaybillApiCommand;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeToSaleWaybillApiCommand;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeToEvotorApiCommand;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeToBillApiCommand;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeToStockOperationsApiCommand;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeFromStockOperations;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeToCurrencyInvoicesApiCommand;
        
        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeToCommissionAgentReportsApiCommand;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeFromCurrencyInvoices;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeFromCommissionAgentReports;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeToProductIncome;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeResultFromProductIncome;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeToRetailRefund;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeResultFromRetailRefund;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeResultFromSalesUpd;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeResultFromPurchaseUpd;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeRequestToSalesUpd;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeRequestToPurchaseUpd;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeResultFromSalesInvoice;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeResultFromPurchaseInvoice;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeRequestToSalesInvoice;

        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeRequestToPurchaseInvoice;

        public static readonly EventBusEventDefinition<ProductImportStartParsingEvent> ProductImportStartParsingCommand;

        // Мерж авансового отчёта
        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeRequestToAdvanceStatement;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeResultFromAdvanceStatement;

        // Мерж счёта-фактуры
        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeRequestToInvoice;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeResultFromInvoice;

        // Мерж заказа от покупателя
        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeRequestToCustomerOrder;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeResultFromCustomerOrder;

        // Мерж заказа поставщику
        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeRequestToSupplierOrder;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeResultFromSupplierOrder;

        // Мерж сборки заказа
        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeRequestToCustomerOrderPack;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeResultFromCustomerOrderPack;

        // Запуск генерации Движения
        public static readonly EventBusEventDefinition<StockOperationsGenerationEvent> GenerateStockOperationsApiCommand;

        // обновление сабконто в проводках
        public static readonly EventBusEventDefinition<ProductMergeEvent> ProductMergeRequestToPostingsSubconto;

        public static readonly EventBusEventDefinition<ProductMergeResultEvent> ProductMergeResultFromPostingsSubconto;

        // Запуск генерации Движения (v2)
        public static readonly EventBusEventDefinition<StartStockOperationsGenerationCommand> StartStockOperationsGenerationCommand;
    }
}