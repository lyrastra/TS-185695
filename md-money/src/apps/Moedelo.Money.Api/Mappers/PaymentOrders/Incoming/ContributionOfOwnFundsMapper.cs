using Moedelo.Money.Api.Models.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Enums;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class ContributionOfOwnFundsMapper
    {
        public static ContributionOfOwnFundsResponseDto Map(ContributionOfOwnFundsResponse response)
        {
            return new ContributionOfOwnFundsResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport,
                OperationState = response.OperationState
            };
        }

        public static ContributionOfOwnFundsSaveRequest Map(ContributionOfOwnFundsSaveDto dto)
        {
            return new ContributionOfOwnFundsSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Sum = dto.Sum,
                Description = dto.Description ?? PaymentOrdersDescriptions.ContributionOfOwnFunds
            };
        }

        public static ContributionOfOwnFundsSaveRequest ToSaveRequest(this ConfirmContributionOfOwnFundsDto dto)
        {
            return new ContributionOfOwnFundsSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Sum = dto.Sum,
                Description = dto.Description,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
            };
        }
    }
}
