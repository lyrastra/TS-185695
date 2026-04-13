using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.TaxPostings.Enums;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public static class UnifiedBudgetaryPaymentMapper
    {
        public static UnifiedBudgetaryPaymentResponseDto Map(UnifiedBudgetaryPaymentResponse response)
        {
            return new UnifiedBudgetaryPaymentResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsReadOnly = response.IsReadOnly,
                IsPaid = response.IsPaid,
                Uin = response.Uin,
                AccountCode = response.AccountCode,
                KbkId = response.KbkId,
                KbkNumber = response.KbkNumber,
                PayerStatus = response.PayerStatus,
                Recipient = new UnifiedBudgetaryRecipientResponseDto
                {
                    Name = response.Recipient.Name,
                    Inn = response.Recipient.Inn,
                    Kpp = response.Recipient.Kpp,
                    SettlementAccount = response.Recipient.SettlementAccount,
                    BankBik = response.Recipient.BankBik,
                    BankCorrespondentAccount = response.Recipient.UnifiedSettlementAccount,
                    BankName = response.Recipient.BankName,
                    Oktmo = response.Recipient.Oktmo
                },
                SubPayments = response.SubPayments.Select(MapToDto).ToArray(),
                OperationState = response.OperationState
            };
        }

        public static UnifiedBudgetarySubPaymentResponseDto MapToDto(UnifiedBudgetarySubPayment response)
        {
            return new UnifiedBudgetarySubPaymentResponseDto
            {
                DocumentBaseId = response.DocumentBaseId,
                ParentDocumentId = response.ParentDocumentId,
                Sum = response.Sum,
                TradingObjectId = response.TradingObjectId,
                PatentId = response.PatentId,
                Kbk = new UnifiedBudgetaryKbkResponseDto
                {
                    Id = response.Kbk.Id,
                    Number = response.Kbk.Number,
                    AccountCode = response.Kbk.AccountCode.GetValueOrDefault()
                },
                CurrencyInvoices = MapCurrencyInvoices(response.CurrencyInvoices),
                Period = BudgetaryPeriodMapper.MapToDto(response.Period)
            };
        }

        public static UnifiedBudgetarySubPaymentResponsePrivateDto MapToPrivateDto(UnifiedBudgetarySubPayment response)
        {
            return new UnifiedBudgetarySubPaymentResponsePrivateDto
            {
                DocumentBaseId = response.DocumentBaseId,
                ParentDocumentId = response.ParentDocumentId,
                Sum = response.Sum,
                TradingObjectId = response.TradingObjectId,
                PatentId = response.PatentId,
                KbkId = response.Kbk.Id,
                Period = BudgetaryPeriodMapper.MapToDto(response.Period)
            };
        }

        public static UnifiedBudgetaryPaymentSaveRequest Map(UnifiedBudgetaryPaymentSaveDto dto)
        {
            return new UnifiedBudgetaryPaymentSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                ProvideInAccounting = dto.IsPaid && (dto.ProvideInAccounting ?? true),
                IsPaid = dto.IsPaid,
                Uin = dto.Uin,
                PayerStatus = dto.PayerStatus,
                Recipient = new UnifiedBudgetaryRecipient
                {
                    Inn = dto.Recipient.Inn,
                    BankBik = dto.Recipient.BankBik,
                    UnifiedSettlementAccount = dto.Recipient.BankCorrespondentAccount,
                    BankName = dto.Recipient.BankName,
                    Kpp = dto.Recipient.Kpp,
                    Name = dto.Recipient.Name,
                    Oktmo = dto.Recipient.Oktmo,
                    SettlementAccount = dto.Recipient.SettlementAccount
                },
                SubPayments = dto.SubPayments.Select(sub => new UnifiedBudgetarySubPaymentSaveModel 
                {
                    DocumentBaseId = sub.DocumentBaseId.GetValueOrDefault(),
                    KbkId = sub.KbkId,
                    Period = BudgetaryPeriodMapper.MapToDomain(sub.Period),
                    Sum = sub.Sum,
                    PatentId = sub.PatentId,
                    TradingObjectId = sub.TradingObjectId,
                    TaxPostings = TaxPostingsMapper.MapTaxPostings(sub.TaxPostings, TaxPostingDirection.Outgoing),
                    CurrencyInvoices = LinkedDocumentsMapper.MapDocumentLinks(sub.CurrencyInvoices)

                }).ToArray(),
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }
        
        public static PaymentDetailDto MapToPaymentDetail(UnifiedBudgetaryPaymentResponse response)
        {
            return new PaymentDetailDto
            {
                Inn = response.Recipient.Inn,
                PayeeSettlementAccount = response.Recipient.SettlementAccount,
                Amount = response.Sum,
                Number = response.Number
            };
        }


        internal static UnifiedBudgetaryPaymentKbkDefaultsRequest MapToDomain(UnifiedBudgetaryPaymentKbkDefaultsRequestDto dto)
        {
            return new UnifiedBudgetaryPaymentKbkDefaultsRequest
            {
                Date = dto.Date,
                TradingObjectId = dto.TradingObjectId,
                Period = BudgetaryPeriodMapper.MapToDomain(dto.Period)
            };
        }

        internal static IReadOnlyCollection<UnifiedBudgetarySubPaymentForDescription> MapToDomain(IReadOnlyCollection<UnifiedBudgetarySubPaymentDto> dto)
        {
            return dto.Select(d=>  new UnifiedBudgetarySubPaymentForDescription
            {
                KbkId = d.KbkId,
                Period = BudgetaryPeriodMapper.MapToDomain(d.Period),
                Sum = d.Sum,
                PatentId = d.PatentId,
                TradingObjectId = d.TradingObjectId,
                
            }).ToArray();
        }

        private static RemoteServiceResponseDto<IReadOnlyCollection<CurrencyInvoiceResponseDto>> MapCurrencyInvoices(RemoteServiceResponse<IReadOnlyCollection<CurrencyInvoiceLink>> response)
        {
            return new RemoteServiceResponseDto<IReadOnlyCollection<CurrencyInvoiceResponseDto>>
            {
                Data = response.Data?.Select(link =>
                    new CurrencyInvoiceResponseDto
                    {
                        DocumentBaseId = link.DocumentBaseId,
                        Date = link.DocumentDate,
                        Number = link.DocumentNumber,
                        LinkSum = link.LinkSum
                    }).ToArray(),
                Status = response.Status
            };
        }

        internal static UnifiedBudgetaryPaymentSaveRequest ToSaveRequest(this ConfirmUnifiedBudgetaryPaymentDto dto)
        {
            return new UnifiedBudgetaryPaymentSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                PayerStatus = BudgetaryPayerStatus.Company,
                ProvideInAccounting = true,
                IsPaid = true,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
                // устанавливается в UnifiedBudgetaryPaymentOutsourceProcessor
                Recipient = null,
                SubPayments = null,
            };
        }
    }
}