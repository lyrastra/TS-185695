using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy
{
    public interface IKontragentSettlementAccountsApiClient
    {
        Task<long> SaveAsync(FirmId firmId, UserId userId, KontragentSettlementAccountDto settlementAccount);
        Task<List<KontragentSettlementAccountDto>> GetByKontragentAsync(FirmId firmId, UserId userId, int kontragentId);

    }
}