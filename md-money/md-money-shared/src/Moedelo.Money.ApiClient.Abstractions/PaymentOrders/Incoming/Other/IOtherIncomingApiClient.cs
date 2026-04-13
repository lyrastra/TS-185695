using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.Other.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.Other
{
    public interface IOtherIncomingApiClient
    {
        Task<OtherIncomingDto> GetByBaseIdAsync(long documentBaseId);

        Task<OtherIncomingDto[]> GetByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}