using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    static class CurrencyTransferFromAccountMapper
    {
        public static CurrencyTransferFromAccountResponse MapToResponse(CurrencyTransferFromAccountDto dto)
        {
            return new CurrencyTransferFromAccountResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                FromSettlementAccountId = dto.FromSettlementAccountId,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                DuplicateId = dto.DuplicateId,
                ProvideInAccounting = dto.ProvideInAccounting,
                OperationState = dto.OperationState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static CurrencyTransferFromAccountSaveRequest MapToSaveRequest(CurrencyTransferFromAccountResponse response)
        {
            return new CurrencyTransferFromAccountSaveRequest
            {
                Sum = response.Sum,
                Date = response.Date,
                Number = response.Number,
                Description = response.Description,
                DocumentBaseId = response.DocumentBaseId,
                SettlementAccountId = response.SettlementAccountId,
                FromSettlementAccountId = response.FromSettlementAccountId,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static CurrencyTransferFromAccountSaveRequest MapToSaveRequest(CurrencyTransferFromAccountImportRequest request)
        {
            return new CurrencyTransferFromAccountSaveRequest
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                SettlementAccountId = request.SettlementAccountId,
                FromSettlementAccountId = request.FromSettlementAccountId,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                ProvideInAccounting = true,
                SourceFileId = request.SourceFileId,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static CurrencyTransferFromAccountCreatedMessage MapToCreatedMessage(CurrencyTransferFromAccountSaveRequest request)
        {
            return new CurrencyTransferFromAccountCreatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                FromSettlementAccountId = request.FromSettlementAccountId,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static CurrencyTransferFromAccountUpdatedMessage MapToUpdatedMessage(CurrencyTransferFromAccountSaveRequest request)
        {
            return new CurrencyTransferFromAccountUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Number = request.Number,
                Date = request.Date,
                FromSettlementAccountId = request.FromSettlementAccountId.GetValueOrDefault(),
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static CurrencyTransferFromAccountDeletedMessage MapToDeletedMessage(CurrencyTransferFromAccountResponse response, long? newDocumentBaseId)
        {
            return new CurrencyTransferFromAccountDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }
    }
}
