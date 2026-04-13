using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromPurse.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromPurse
{
    internal static class TransferFromPurseMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(TransferFromPurseSaveRequest saveRequest)
        {
            return new BaseDocumentCreateRequest
            {
                Number = saveRequest.Number,
                Date = saveRequest.Date,
                Sum = saveRequest.Sum
            };
        }

        internal static TransferFromPurseDto MapToDto(TransferFromPurseSaveRequest request)
        {
            return new TransferFromPurseDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState
            };
        }

        internal static TransferFromPurseSaveRequest MapToSaveRequest(TransferFromPurseResponse response)
        {
            return new TransferFromPurseSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState
            };
        }

        internal static TransferFromPurseSaveRequest MapToSaveRequest(TransferFromPurseImportRequest request)
        {
            return new TransferFromPurseSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                ProvideInAccounting = true,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static TransferFromPurseResponse MapToResponse(TransferFromPurseDto dto)
        {
            return new TransferFromPurseResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OutsourceState = dto.OutsourceState
            };
        }

        internal static TransferFromPurseCreatedMessage MapToCreatedMessage(TransferFromPurseSaveRequest request)
        {
            return new TransferFromPurseCreatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
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

        internal static TransferFromPurseUpdatedMessage MapToUpdatedMessage(TransferFromPurseSaveRequest request)
        {
            return new TransferFromPurseUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                SettlementAccountId = request.SettlementAccountId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState
            };
        }

        internal static TransferFromPurseDeletedMessage MapToDeletedMessage(TransferFromPurseResponse response, long? newDocumentBaseId)
        {
            return new TransferFromPurseDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Number = response.Number,
                Date = response.Date,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }
    }
}
