using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class LoanRepaymentMapper
    {
        public static LoanRepaymentResponseDto Map(LoanRepaymentResponse response)
        {
            return new LoanRepaymentResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                IsLongTermLoan = response.IsLongTermLoan,
                LoanInterestSum = response.LoanInterestSum,
                ProvideInAccounting = response.ProvideInAccounting,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport,
            };
        }

        public static LoanRepaymentSaveRequest Map(LoanRepaymentSaveDto dto)
        {
            return new LoanRepaymentSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = dto.Contract?.DocumentBaseId ?? 0L,
                Sum = dto.Sum,
                Description = dto.Description,
                IsLongTermLoan = dto.IsLongTermLoan,
                LoanInterestSum = dto.LoanInterestSum,
                ProvideInAccounting = dto.IsPaid ? dto.ProvideInAccounting ?? true : false,
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Outgoing),
                IsPaid = dto.IsPaid,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }
        
        public static PaymentDetailDto MapToPaymentDetail(LoanRepaymentResponse response)
        {
            return new PaymentDetailDto
            {
                Inn = response.Kontragent.Inn,
                PayeeSettlementAccount = response.Kontragent.SettlementAccount,
                Amount = response.Sum,
                Number = response.Number
            };
        }

        internal static LoanRepaymentSaveRequest ToSaveRequest(this ConfirmLoanRepaymentDto dto)
        {
            return new LoanRepaymentSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                Sum = dto.Sum,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                LoanInterestSum = dto.LoanInterestSum,
                TaxPostings = new TaxPostingsData(),
                IsPaid = true,
                ProvideInAccounting = true,
                IsLongTermLoan = false,
                ContractBaseId = 0,
            };
        } 
    }
}
