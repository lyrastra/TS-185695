using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount
{
    internal static class TransferToAccountMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(TransferToAccountSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static TransferToAccountDto MapToDto(TransferToAccountSaveRequest request)
        {
            return new TransferToAccountDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                ToSettlementAccountId = request.ToSettlementAccountId,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                SourceFileId = request.SourceFileId,
                IsIgnoreNumber = request.IsIgnoreNumber,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static TransferToAccountSaveRequest MapToSaveRequest(TransferToAccountResponse response)
        {
            return new TransferToAccountSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                ToSettlementAccountId = response.ToSettlementAccountId,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static TransferToAccountSaveRequest MapToSaveRequest(TransferToAccountImportRequest request)
        {
            return new TransferToAccountSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                ToSettlementAccountId = request.ToSettlementAccountId,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                ProvideInAccounting = true,
                IsPaid = true,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                SourceFileId = request.SourceFileId,
                IsIgnoreNumber = request.IsIgnoreNumber,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        public static TransferToAccountResponse MapToResponse(TransferToAccountDto dto)
        {
            return new TransferToAccountResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                ToSettlementAccountId = dto.ToSettlementAccountId,
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsPaid = dto.IsPaid,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static TransferFromAccountSaveRequest MapToTransferFromAccountSaveRequest(TransferToAccountSaveRequest request)
        {
            return new TransferFromAccountSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                FromSettlementAccountId = request.SettlementAccountId,
                SettlementAccountId = request.ToSettlementAccountId.GetValueOrDefault(),
                Description = request.Description
            };
        }

        internal static TransferToAccountCreated MapToCreatedMessage(TransferToAccountSaveRequest request)
        {
            return new TransferToAccountCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                ToSettlementAccountId = request.ToSettlementAccountId.GetValueOrDefault(),
                TransferFromAccountBaseId = request.TransferFromAccountBaseId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static TransferToAccountUpdated MapToUpdatedMessage(TransferToAccountSaveRequest request)
        {
            return new TransferToAccountUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                ToSettlementAccountId = request.ToSettlementAccountId.GetValueOrDefault(),
                TransferFromAccountBaseId = request.TransferFromAccountBaseId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static TransferToAccountProvideRequired MapToProvideRequired(TransferToAccountResponse response)
        {
            return new TransferToAccountProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                ToSettlementAccountId = response.ToSettlementAccountId.GetValueOrDefault(),
                Sum = response.Sum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid
            };
        }

        internal static TransferToAccountDeleted MapToDeleted(TransferToAccountResponse response, long? newDocumentBaseId)
        {
            return new TransferToAccountDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }
    }
}
