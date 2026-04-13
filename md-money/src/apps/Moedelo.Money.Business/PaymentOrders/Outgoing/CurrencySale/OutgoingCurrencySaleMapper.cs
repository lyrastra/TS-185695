using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale.Events;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencySale
{
    public static class OutgoingCurrencySaleMapper
    {
        internal static OutgoingCurrencySaleSaveRequest MapToSaveRequest(OutgoingCurrencySaleResponse response)
        {
            return new OutgoingCurrencySaleSaveRequest
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

        internal static OutgoingCurrencySaleSaveRequest MapToSaveRequest(OutgoingCurrencySaleImportRequest request)
        {
            return new OutgoingCurrencySaleSaveRequest
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

        internal static OutgoingCurrencySaleCreated MapToCreatedMessage(OutgoingCurrencySaleSaveRequest request)
        {
            return new OutgoingCurrencySaleCreated
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
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static OutgoingCurrencySaleUpdated MapToUpdatedMessage(OutgoingCurrencySaleSaveRequest request)
        {
            return new OutgoingCurrencySaleUpdated
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
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        public static OutgoingCurrencySaleProvideRequired MapToProvideRequired(OutgoingCurrencySaleResponse response)
        {
            return new OutgoingCurrencySaleProvideRequired
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
                ProvideInAccounting = response.ProvideInAccounting,
                IsManualTaxPostings = response.TaxPostingsInManualMode
            };
        }

        internal static OutgoingCurrencySaleDeleted MapToDeletedMessage(OutgoingCurrencySaleResponse response, long? newDocumentBaseId)
        {
            return new OutgoingCurrencySaleDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            OutgoingCurrencySaleSaveRequest request)
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
