using Moedelo.Money.Api.Models.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class TransferFromAccountMapper
    {
        public static TransferFromAccountResponseDto Map(TransferFromAccountResponse response)
        {
            return new TransferFromAccountResponseDto
            {
                Number = response.Number,
                Date = response.Date.Date,
                FromSettlementAccountId = response.FromSettlementAccountId,
                SettlementAccountId = response.SettlementAccountId,
                Sum = response.Sum,
                Description = response.Description,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport,
                OperationState = response.OperationState
            };
        }

        public static TransferFromAccountSaveRequest Map(TransferFromAccountSaveDto saveDto)
        {
            return new TransferFromAccountSaveRequest
            {
                Date = saveDto.Date.Date,
                Number = saveDto.Number,
                FromSettlementAccountId = saveDto.FromSettlementAccountId,
                SettlementAccountId = saveDto.SettlementAccountId,
                Sum = saveDto.Sum,
                Description = saveDto.Description
            };
        }

        public static TransferFromAccountSaveRequest ToSaveRequest(this ConfirmTransferFromAccountDto dto)
        {
            return new TransferFromAccountSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                FromSettlementAccountId = dto.FromSettlementAccountId ?? 0,
                SettlementAccountId = dto.SettlementAccountId,
                Sum = dto.Sum,
                Description = dto.Description,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
            };
        }
    }
}