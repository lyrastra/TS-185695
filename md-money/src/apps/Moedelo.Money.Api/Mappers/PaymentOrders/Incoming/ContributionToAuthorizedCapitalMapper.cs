using Moedelo.Money.Api.Models.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class ContributionToAuthorizedCapitalMapper
    {
        public static ContributionToAuthorizedCapitalResponseDto Map(ContributionToAuthorizedCapitalResponse response)
        {
            return new ContributionToAuthorizedCapitalResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Sum = response.Sum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        public static ContributionToAuthorizedCapitalSaveRequest Map(ContributionToAuthorizedCapitalSaveDto dto)
        {
            return new ContributionToAuthorizedCapitalSaveRequest
            {
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
            };
        }

        public static ContributionToAuthorizedCapitalSaveRequest ToSaveRequest(this ConfirmContributionToAuthorizedCapitalDto dto)
        {
            return new ContributionToAuthorizedCapitalSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ProvideInAccounting = true,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null
            };
        }
    }
}
