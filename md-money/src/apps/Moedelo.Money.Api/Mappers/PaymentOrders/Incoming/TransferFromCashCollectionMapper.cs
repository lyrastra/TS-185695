using Moedelo.Money.Api.Models.PaymentOrders.Incoming.TransferFromCashCollection;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCashCollection;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class TransferFromCashCollectionMapper
    {
        public static TransferFromCashCollectionResponseDto Map(TransferFromCashCollectionResponse response)
        {
            return new TransferFromCashCollectionResponseDto
            {
                Number = response.Number,
                Date = response.Date.Date,
                SettlementAccountId = response.SettlementAccountId,
                Sum = response.Sum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        public static TransferFromCashCollectionSaveRequest Map(TransferFromCashCollectionSaveDto saveDto)
        {
            return new TransferFromCashCollectionSaveRequest
            {
                DocumentBaseId = 0,
                Date = saveDto.Date.Date,
                Sum = saveDto.Sum,
                Number = saveDto.Number,
                Description = saveDto.Description,
                SettlementAccountId = saveDto.SettlementAccountId,
                ProvideInAccounting = saveDto.ProvideInAccounting ?? true,
            };
        }

        public static TransferFromCashCollectionSaveRequest ToSaveRequest(this ConfirmTransferFromCashCollectionDto dto)
        {
            return new TransferFromCashCollectionSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Sum = dto.Sum,
                Number = dto.Number,
                Description = dto.Description,
                SettlementAccountId = dto.SettlementAccountId,
                ProvideInAccounting = true,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null
            };
        }
    }
}