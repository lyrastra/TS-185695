using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash
{
    internal static class TransferFromCashMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(TransferFromCashSaveRequest saveRequest)
        {
            return new BaseDocumentCreateRequest
            {
                Number = saveRequest.Number,
                Date = saveRequest.Date,
                Sum = saveRequest.Sum
            };
        }

        internal static TransferFromCashDto MapToDto(TransferFromCashSaveRequest request)
        {
            return new TransferFromCashDto
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
                OutsourceState = request.OutsourceState,
            };
        }

        internal static TransferFromCashSaveRequest MapToSaveRequest(TransferFromCashResponse response)
        {
            return new TransferFromCashSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                CashOrderBaseId = response.CashOrder.GetOrThrow()?.DocumentBaseId,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static TransferFromCashSaveRequest MapToSaveRequest(TransferFromCashImportRequest request)
        {
            return new TransferFromCashSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                ProvideInAccounting = true,
                CashOrderBaseId = request.CashOrderBaseId,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                SourceFileId = request.SourceFileId,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static TransferFromCashResponse MapToResponse(TransferFromCashDto dto)
        {
            return new TransferFromCashResponse
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
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static TransferFromCashCreatedMessage MapToCreatedMessage(TransferFromCashSaveRequest request)
        {
            return new TransferFromCashCreatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                CashOrderBaseId = request.CashOrderBaseId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static TransferFromCashUpdatedMessage MapToUpdatedMessage(TransferFromCashSaveRequest request)
        {
            return new TransferFromCashUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                SettlementAccountId = request.SettlementAccountId,
                CashOrderBaseId = request.CashOrderBaseId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static TransferFromCashDeletedMessage MapToDeletedMessage(TransferFromCashResponse response, long? newDocumentBaseId)
        {
            return new TransferFromCashDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Number = response.Number,
                Date = response.Date,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }
    }
}
