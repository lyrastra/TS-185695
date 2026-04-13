using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromAccount
{
    internal static class TransferFromAccountMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(TransferFromAccountSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static TransferFromAccountDto MapToDto(TransferFromAccountSaveRequest request)
        {
            return new TransferFromAccountDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                FromSettlementAccountId = request.FromSettlementAccountId,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static TransferFromAccountSaveRequest MapToSaveRequest(TransferFromAccountResponse response)
        {
            return new TransferFromAccountSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                FromSettlementAccountId = response.FromSettlementAccountId,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static TransferFromAccountSaveRequest MapToSaveRequest(TransferFromAccountImportRequest request)
        {
            return new TransferFromAccountSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                FromSettlementAccountId = request.FromSettlementAccountId,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static TransferFromAccountResponse MapToResponse(TransferFromAccountDto dto)
        {
            return new TransferFromAccountResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                FromSettlementAccountId = dto.FromSettlementAccountId,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId)
            };
        }

        internal static TransferFromAccountSaveRequest Map(TransferFromAccountResponse request)
        {
            return new TransferFromAccountSaveRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                FromSettlementAccountId = request.FromSettlementAccountId,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static TransferFromAccountCreatedMessage MapToCreatedMessage(TransferFromAccountSaveRequest request)
        {
            return new TransferFromAccountCreatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                FromSettlementAccountId = request.FromSettlementAccountId,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static TransferFromAccountUpdatedMessage MapToUpdatedMessage(TransferFromAccountSaveRequest request)
        {
            return new TransferFromAccountUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Number = request.Number,
                Date = request.Date,
                FromSettlementAccountId = request.FromSettlementAccountId,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static TransferFromAccountDeletedMessage MapToDeletedMessage(TransferFromAccountResponse response, long? newDocumentBaseId)
        {
            return new TransferFromAccountDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Number = response.Number,
                Date = response.Date,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }
    }
}
