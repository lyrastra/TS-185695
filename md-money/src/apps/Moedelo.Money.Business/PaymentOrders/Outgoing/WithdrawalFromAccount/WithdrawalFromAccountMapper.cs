using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    internal static class WithdrawalFromAccountMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(WithdrawalFromAccountSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static WithdrawalFromAccountDto MapToDto(WithdrawalFromAccountSaveRequest operation)
        {
            return new WithdrawalFromAccountDto
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Number = operation.Number,
                Sum = operation.Sum,
                SettlementAccountId = operation.SettlementAccountId,
                Description = operation.Description,
                ProvideInAccounting = operation.ProvideInAccounting,
                CashOrderBaseId = operation.CashOrderBaseId,
                DuplicateId = operation.DuplicateId,
                SourceFileId = operation.SourceFileId,
                OperationState = operation.OperationState,
                OutsourceState = operation.OutsourceState,
                IsIgnoreNumber = operation.IsIgnoreNumber
            };
        }

        internal static WithdrawalFromAccountResponse MapToResponse(WithdrawalFromAccountDto dto)
        {
            return new WithdrawalFromAccountResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId)
            };
        }

        internal static WithdrawalFromAccountSaveRequest MapToSaveRequest(WithdrawalFromAccountResponse response)
        {
            return new WithdrawalFromAccountSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                Description = response.Description,
                SettlementAccountId = response.SettlementAccountId,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
                ProvideInAccounting = response.ProvideInAccounting,
            };
        }

        internal static WithdrawalFromAccountSaveRequest MapToSaveRequest(WithdrawalFromAccountImportRequest request)
        {
            return new WithdrawalFromAccountSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                Description = request.Description,
                SettlementAccountId = request.SettlementAccountId,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                CashOrderBaseId = request.CashOrderBaseId,
                SourceFileId = request.SourceFileId,
                ProvideInAccounting = true,
                IsIgnoreNumber = request.IsIgnoreNumber,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static WithdrawalFromAccountCreated MapToCreatedMessage(WithdrawalFromAccountSaveRequest request)
        {
            return new WithdrawalFromAccountCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                CashOrderBaseId = request.CashOrderBaseId,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static WithdrawalFromAccountUpdated MapToUpdatedMessage(WithdrawalFromAccountSaveRequest request)
        {
            return new WithdrawalFromAccountUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                CashOrderBaseId = request.CashOrderBaseId,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static WithdrawalFromAccountProvideRequired MapToProvideRequired(WithdrawalFromAccountResponse response)
        {
            return new WithdrawalFromAccountProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Sum = response.Sum,
                Description = response.Description,
                CashOrderBaseId = response.CashOrder.GetOrThrow()?.DocumentBaseId,
                ProvideInAccounting = response.ProvideInAccounting
            };
        }

        internal static WithdrawalFromAccountDeleted MapToDeleted(WithdrawalFromAccountResponse response, long? newDocumentBaseId)
        {
            return new WithdrawalFromAccountDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }
    }
}
