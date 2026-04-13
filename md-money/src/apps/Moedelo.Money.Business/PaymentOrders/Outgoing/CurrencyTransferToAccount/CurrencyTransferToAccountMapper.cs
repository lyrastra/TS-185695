using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    internal sealed class CurrencyTransferToAccountMapper
    {
        public static CurrencyTransferToAccountResponse MapToResponse(CurrencyTransferToAccountDto dto)
        {
            return new CurrencyTransferToAccountResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                ToSettlementAccountId = dto.ToSettlementAccountId,
                Description = dto.Description,
                DuplicateId = dto.DuplicateId,
                ProvideInAccounting = dto.ProvideInAccounting,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId)
            };
        }

        internal static CurrencyTransferToAccountSaveRequest MapToSaveRequest(CurrencyTransferToAccountResponse response)
        {
            return new CurrencyTransferToAccountSaveRequest
            {
                Sum = response.Sum,
                Date = response.Date,
                Number = response.Number,
                Description = response.Description,
                DocumentBaseId = response.DocumentBaseId,
                SettlementAccountId = response.SettlementAccountId,
                ToSettlementAccountId = response.ToSettlementAccountId,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
                ProvideInAccounting = response.ProvideInAccounting,
            };
        }

        internal static CurrencyTransferToAccountSaveRequest MapToSaveRequest(CurrencyTransferToAccountImportRequest request)
        {
            return new CurrencyTransferToAccountSaveRequest
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                SettlementAccountId = request.SettlementAccountId,
                ToSettlementAccountId = request.ToSettlementAccountId,
                SourceFileId = request.SourceFileId,
                DuplicateId = request.DuplicateId,
                ProvideInAccounting = true,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static CurrencyTransferToAccountCreatedMessage MapToCreatedMessage(CurrencyTransferToAccountSaveRequest request)
        {
            return new CurrencyTransferToAccountCreatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                ToSettlementAccountId = request.ToSettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static CurrencyTransferToAccountUpdatedMessage MapToUpdatedMessage(CurrencyTransferToAccountSaveRequest request)
        {
            return new CurrencyTransferToAccountUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                ToSettlementAccountId = request.ToSettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static CurrencyTransferToAccountDeletedMessage MapToDeletedMessage(CurrencyTransferToAccountResponse response, long? newDocumentBaseId)
        {
            return new CurrencyTransferToAccountDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }
    }
}