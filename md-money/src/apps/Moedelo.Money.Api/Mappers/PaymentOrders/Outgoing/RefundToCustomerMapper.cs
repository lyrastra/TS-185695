using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Enums;
using Moedelo.Money.Resources;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class RefundToCustomerMapper
    {
        public static RefundToCustomerResponseDto Map(RefundToCustomerResponse response)
        {
            return new RefundToCustomerResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                Nds = MapNds(response),
                IsMainContractor = response.IsMainContractor,
                ProvideInAccounting = response.ProvideInAccounting,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly,
                TaxationSystemType = response.TaxationSystemType,
                PatentId = response.PatentId,
                IsFromImport = response.IsFromImport
            };
        }
        
        public static PaymentDetailDto MapToPaymentDetail(RefundToCustomerResponse response)
        {
            return new PaymentDetailDto
            {
                Inn = response.Kontragent.Inn,
                PayeeSettlementAccount = response.Kontragent.SettlementAccount,
                Amount = response.Sum,
                Number = response.Number
            };
        }

        private static NdsResponseDto MapNds(RefundToCustomerResponse operation)
        {
            return new NdsResponseDto
            {
                IncludeNds = operation.IncludeNds,
                Type = operation.IncludeNds
                    ? operation.NdsType
                    : null,
                Sum = operation.IncludeNds
                    ? operation.NdsSum
                    : null
            };
        }

        public static RefundToCustomerSaveRequest Map(RefundToCustomerSaveDto dto)
        {
            return new RefundToCustomerSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = dto.Contract?.DocumentBaseId,
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.RefundToCustomer
                    : dto.Description,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Type
                    : null,
                NdsSum = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Sum
                    : null,
                IsMainContractor = dto.IsMainContractor ?? true,
                ProvideInAccounting = dto.IsPaid && (dto.ProvideInAccounting ?? true),
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Incoming),
                IsPaid = dto.IsPaid,
                TaxationSystemType = dto.TaxationSystemType,
                PatentId = dto.PatentId,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

        public static RefundToCustomerSaveRequest ToSaveRequest(this ConfirmRefundToCustomerDto dto)
        {
            return new RefundToCustomerSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = null,
                Description = dto.Description,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds == true ? dto.Nds.Type : null,
                NdsSum = dto.Nds?.IncludeNds == true ? dto.Nds.Sum : null,
                IsMainContractor = true,
                ProvideInAccounting = true,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Incoming),
                IsPaid = true,
                TaxationSystemType = dto.TaxationSystemType,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null
            };
        }
    }
}
