using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Enums;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class AgencyContractMapper
    {
        public static AgencyContractResponseDto Map(AgencyContractResponse response)
        {
            return new AgencyContractResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        public static AgencyContractSaveRequest Map(AgencyContractSaveDto dto)
        {
            return new AgencyContractSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = dto.Contract.DocumentBaseId ?? 0L,
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.AgencyContract
                    : dto.Description,
                ProvideInAccounting = dto.IsPaid && (dto.ProvideInAccounting ?? true),
                IsPaid = dto.IsPaid,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }
        
        public static PaymentDetailDto MapToPaymentDetail(AgencyContractResponse response)
        {
            return new PaymentDetailDto
            {
                Inn = response.Kontragent.Inn,
                PayeeSettlementAccount = response.Kontragent.SettlementAccount,
                Amount = response.Sum,
                Number = response.Number
            };
        }

        internal static AgencyContractSaveRequest ToSaveRequest(this ConfirmAgencyContractDto dto)
        {
            return new AgencyContractSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = 0L,
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.AgencyContract
                    : dto.Description,
                ProvideInAccounting = true,
                IsPaid = true,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
            };
        }
    }
}
