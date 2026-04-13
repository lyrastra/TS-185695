using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.BudgetaryPayment
{
    public static class BudgetaryPaymentAutocompleteMapper
    {
        public static CurrencyInvoiceNdsPaymentsAutocompleteResponseDto[] MapToDto(IReadOnlyCollection<CurrencyInvoiceNdsPaymentsAutocompleteResponse> response)
        {
            return response.Select(p => new CurrencyInvoiceNdsPaymentsAutocompleteResponseDto
            {
                Id = p.Id,
                Date = p.Date,
                Number = p.Number,
                Sum = p.Sum
            }).ToArray();
        }

        public static CurrencyInvoiceNdsPaymentsAutocompleteRequest MapToDomain(CurrencyInvoiceNdsPaymentsAutocompleteRequestDto dto)
        {
            return new CurrencyInvoiceNdsPaymentsAutocompleteRequest
            {
                Count = dto.Count,
                Query = dto.Query,
                IsNdsPaidAtCustoms = dto.IsNdsPaidAtCustoms,
                CurrencyInvoiceBaseId = dto.CurrencyInvoiceBaseId
            };
        }
    }
}