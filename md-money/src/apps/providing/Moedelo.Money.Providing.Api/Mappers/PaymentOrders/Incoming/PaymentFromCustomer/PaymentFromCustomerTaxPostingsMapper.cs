using Moedelo.Money.Providing.Api.Models.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Requisites.Enums.TaxationSystems;

namespace Moedelo.Money.Providing.Api.Mappers.PaymentOrders.Incoming.PaymentFromCustomer
{
    internal static class PaymentFromCustomerTaxPostingsMapper
    {
        public static PaymentFromCustomerTaxPostingsGenerateRequest Map(PaymentFromCustomerTaxPostingsGenerateRequestDto dto)
        {
            return new PaymentFromCustomerTaxPostingsGenerateRequest
            {
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                IncludeNds = dto.Nds != null,
                NdsSum = dto.Nds?.Sum,
                MediationIncludeNds = dto.MediationNds != null,
                MediationNdsSum = dto.MediationNds?.Sum,
                KontragentId = dto.Contractor.Id,
                KontragentName = dto.Contractor.Name,
                IsMediation = dto.Mediation?.IsMediation ?? false,
                MediationCommissionSum = dto.Mediation?.IsMediation ?? false
                    ? dto.Mediation?.CommissionSum
                    : default,
                DocumentLinks = LinkedDocumentsMapper.Map(dto.Documents),
                TaxationSystemType = (TaxationSystemType?) dto.TaxationSystemType,
            };
        }
    }
}
