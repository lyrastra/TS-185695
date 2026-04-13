using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public interface IPaymentToNaturalPersonsApiClient
    {
        Task<PaymentToNaturalPersonsResponseDto[]> GetByBaseIdsIdAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}