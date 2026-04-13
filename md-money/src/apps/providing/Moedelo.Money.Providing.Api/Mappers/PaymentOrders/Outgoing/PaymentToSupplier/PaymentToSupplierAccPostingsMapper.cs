using Moedelo.Money.Providing.Api.Models.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models;

namespace Moedelo.Money.Providing.Api.Mappers.PaymentOrders.Outgoing.PaymentToSupplier
{
    static class PaymentToSupplierAccPostingsMapper
    {
        public static PaymentToSupplierAccPostingsFullGenerateRequest Map(PaymentToSupplierAccPostingsGenerateRequestDto dto)
        {
            return new PaymentToSupplierAccPostingsFullGenerateRequest
            {
                Date = dto.Date,
                Sum = dto.Sum,
                IsMainKontragent = dto.IsMainContractor ?? true,
                SettlementAccountId = dto.SettlementAccountId,
                KontragentId = dto.Contractor.Id,
                ContractBaseId = dto.Contract?.DocumentBaseId,
                DocumentLinks = LinkedDocumentsMapper.Map(dto.Documents),
            };
        }
    }
}
