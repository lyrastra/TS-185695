using Moedelo.Money.Providing.Api.Models.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Models;

namespace Moedelo.Money.Providing.Api.Mappers.PaymentOrders.Incoming.PaymentFromCustomer
{
    public class PaymentFromCustomerAccPostingsMapper
    {
        public static PaymentFromCustomerAccPostingsFullGenerateRequest Map(PaymentFromCustomerAccPostingsGenerateRequestDto dto)
        {
            return new PaymentFromCustomerAccPostingsFullGenerateRequest
            {
                Date = dto.Date,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                KontragentId = dto.Contractor.Id,
                ContractBaseId = dto.Contract?.DocumentBaseId,
                IsMediation = dto.Mediation?.IsMediation ?? false,
                DocumentLinks = LinkedDocumentsMapper.Map(dto.Documents),
                IsMainKontragent = dto.IsMainContractor ?? true,
            };
        }
    }
}
