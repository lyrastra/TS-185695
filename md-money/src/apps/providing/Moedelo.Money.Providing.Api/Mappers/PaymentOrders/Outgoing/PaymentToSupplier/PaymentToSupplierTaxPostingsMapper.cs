using Moedelo.Money.Providing.Api.Models.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models;

namespace Moedelo.Money.Providing.Api.Mappers.PaymentOrders.Outgoing.PaymentToSupplier
{
    static class PaymentToSupplierTaxPostingsMapper
    {
        public static PaymentToSupplierTaxPostingsGenerateRequest Map(PaymentToSupplierTaxPostingsGenerateRequestDto dto)
        {
            return new PaymentToSupplierTaxPostingsGenerateRequest
            {
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                IncludeNds = dto.Nds != null,
                NdsSum = dto.Nds?.Sum,
                KontragentId = dto.Contractor.Id,
                KontragentName = dto.Contractor.Name,
                DocumentLinks = LinkedDocumentsMapper.Map(dto.Documents),
                IsPaid = dto.IsPaid
            };
        }
    }
}
