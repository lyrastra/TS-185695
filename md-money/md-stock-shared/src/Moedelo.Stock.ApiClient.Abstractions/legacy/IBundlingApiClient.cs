using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    public interface IBundlingApiClient
    {
        Task<IReadOnlyCollection<BundlingFifoSelfCostSourceDto>> GetSelfCostSourcesAsync(FirmId firmId, UserId userId, SelfCostSourceRequestDto dto);
    }
}