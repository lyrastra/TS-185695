using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase.Events;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPurchase
{
    internal static class OutgoingCurrencyPurchaseMapper
    {
        internal static OutgoingCurrencyPurchaseSaveRequest MapToSaveRequest(OutgoingCurrencyPurchaseResponse response)
        {
            return new OutgoingCurrencyPurchaseSaveRequest
            {
                Sum = response.Sum,
                Date = response.Date,
                Number = response.Number,
                Description = response.Description,
                TotalSum = response.TotalSum,
                ExchangeRate = response.ExchangeRate,
                DocumentBaseId = response.DocumentBaseId,
                ExchangeRateDiff = response.ExchangeRateDiff,
                SettlementAccountId = response.SettlementAccountId,
                ToSettlementAccountId = response.ToSettlementAccountId,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
                ProvideInAccounting = response.ProvideInAccounting,
            };
        }

        internal static OutgoingCurrencyPurchaseSaveRequest MapToSaveRequest(OutgoingCurrencyPurchaseImportRequest request)
        {
            return new OutgoingCurrencyPurchaseSaveRequest
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                TotalSum = request.TotalSum,
                ExchangeRate = request.ExchangeRate,
                ExchangeRateDiff = request.ExchangeRateDiff,
                SettlementAccountId = request.SettlementAccountId,
                ToSettlementAccountId = request.ToSettlementAccountId,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ProvideInAccounting = true,
                TaxPostings = new TaxPostingsData(),
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        public static OutgoingCurrencyPurchaseCreated MapToCreatedMessage(OutgoingCurrencyPurchaseSaveRequest request)
        {
            return new OutgoingCurrencyPurchaseCreated
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                TotalSum = request.TotalSum,
                ExchangeRate = request.ExchangeRate,
                DocumentBaseId = request.DocumentBaseId,
                ExchangeRateDiff = request.ExchangeRateDiff,
                SettlementAccountId = request.SettlementAccountId,
                ToSettlementAccountId = request.ToSettlementAccountId,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ProvideInAccounting = request.ProvideInAccounting,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        public static OutgoingCurrencyPurchaseUpdated MapToUpdatedMessage(OutgoingCurrencyPurchaseSaveRequest request)
        {
            return new OutgoingCurrencyPurchaseUpdated
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                TotalSum = request.TotalSum,
                ExchangeRate = request.ExchangeRate,
                DocumentBaseId = request.DocumentBaseId,
                ExchangeRateDiff = request.ExchangeRateDiff,
                SettlementAccountId = request.SettlementAccountId,
                ToSettlementAccountId = request.ToSettlementAccountId ?? 0,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        public static OutgoingCurrencyPurchaseProvideRequired MapToProvideRequired(OutgoingCurrencyPurchaseResponse response)
        {
            return new OutgoingCurrencyPurchaseProvideRequired
            {
                Sum = response.Sum,
                Date = response.Date,
                Number = response.Number,
                Description = response.Description,
                TotalSum = response.TotalSum,
                ExchangeRate = response.ExchangeRate,
                DocumentBaseId = response.DocumentBaseId,
                ExchangeRateDiff = response.ExchangeRateDiff,
                SettlementAccountId = response.SettlementAccountId,
                ToSettlementAccountId = response.ToSettlementAccountId.Value,
                IsManualTaxPostings = response.TaxPostingsInManualMode,
                ProvideInAccounting = response.ProvideInAccounting
            };
        }

        internal static OutgoingCurrencyPurchaseDeleted MapToDeletedMessage(OutgoingCurrencyPurchaseResponse response, long? newDocumentBaseId)
        {
            return new OutgoingCurrencyPurchaseDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            OutgoingCurrencyPurchaseSaveRequest request)
        {
            return new CustomTaxPostingsOverwriteRequest
            {
                Description = request.Description,
                Postings = request.TaxPostings,
                DocumentDate = request.Date,
                DocumentNumber = request.Number,
                DocumentBaseId = request.DocumentBaseId
            };
        }
    }
}
