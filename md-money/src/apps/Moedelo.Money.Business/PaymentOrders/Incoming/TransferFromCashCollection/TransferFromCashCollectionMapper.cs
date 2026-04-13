using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCashCollection;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCashCollection
{
    internal static class TransferFromCashCollectionMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(TransferFromCashCollectionSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static TransferFromCashCollectionDto MapToDto(TransferFromCashCollectionSaveRequest request)
        {
            return new TransferFromCashCollectionDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static TransferFromCashCollectionSaveRequest MapToSaveRequest(TransferFromCashCollectionResponse response)
        {
            return new TransferFromCashCollectionSaveRequest
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
                OutsourceState = response.OutsourceState,
            };
        }

        internal static TransferFromCashCollectionResponse MapToResponse(TransferFromCashCollectionDto dto)
        {
            return new TransferFromCashCollectionResponse
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
                OutsourceState = dto.OutsourceState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId)
            };
        }

        internal static TransferFromCashCollectionCreatedMessage MapToCreatedMessage(TransferFromCashCollectionSaveRequest request)
        {
            return new TransferFromCashCollectionCreatedMessage
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
            };
        }

        internal static TransferFromCashCollectionUpdatedMessage MapToUpdatedMessage(TransferFromCashCollectionSaveRequest request)
        {
            return new TransferFromCashCollectionUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                SettlementAccountId = request.SettlementAccountId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static TransferFromCashCollectionDeletedMessage MapToDeletedMessage(TransferFromCashCollectionResponse response, long? newDocumentBaseId)
        {
            return new TransferFromCashCollectionDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Number = response.Number,
                Date = response.Date,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }
    }
}
