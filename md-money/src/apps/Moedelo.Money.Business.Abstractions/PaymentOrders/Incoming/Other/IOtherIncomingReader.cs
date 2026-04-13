using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.Other
{
    public interface IOtherIncomingReader
    {
        Task<OtherIncomingResponse> GetByBaseIdAsync(long baseId);

        Task<IReadOnlyCollection<OtherIncomingResponse>> GetByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds);
    }
}
