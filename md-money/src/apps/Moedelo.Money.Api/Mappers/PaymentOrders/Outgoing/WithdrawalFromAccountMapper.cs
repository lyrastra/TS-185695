using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Enums;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class WithdrawalFromAccountMapper
    {
        public static WithdrawalFromAccountResponseDto Map(WithdrawalFromAccountResponse response)
        {
            return new WithdrawalFromAccountResponseDto
            {
                Number = response.Number,
                Date = response.Date.Date,
                SettlementAccountId = response.SettlementAccountId,
                Sum = response.Sum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                CashOrder = MapLinks(response.CashOrder),
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        private static RemoteServiceResponseDto<CashOrderDto> MapLinks(RemoteServiceResponse<CashOrderLink> response)
        {
            return new RemoteServiceResponseDto<CashOrderDto>
            {
                Data = response.Data != null
                    ? new CashOrderDto
                    {
                        DocumentBaseId = response.Data.DocumentBaseId,
                        Number = response.Data.Number,
                        Date = response.Data.Date,
                        Sum = response.Data.Sum
                    }
                    : null,
                Status = response.Status
            };
        }

        public static WithdrawalFromAccountSaveRequest Map(WithdrawalFromAccountSaveDto dto)
        {
            return new WithdrawalFromAccountSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Sum = dto.Sum,
                Number = dto.Number,
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.WithdrawalFromAccount
                    : dto.Description,
                SettlementAccountId = dto.SettlementAccountId,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
                CashOrderBaseId = dto.CashOrder?.DocumentBaseId,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

        public static WithdrawalFromAccountSaveRequest ToSaveRequest(this ConfirmWithdrawalFromAccountDto dto)
        {
            return new WithdrawalFromAccountSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Sum = dto.Sum,
                Number = dto.Number,
                Description = dto.Description,
                SettlementAccountId = dto.SettlementAccountId,
                ProvideInAccounting = true,
                CashOrderBaseId = null,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
            };
        }
    }
}
