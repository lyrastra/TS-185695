using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost.Inventories;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    public interface IInventoryApiClient
    {
        Task<IReadOnlyCollection<InventoryFifoSelfCostSourceDto>> GetSelfCostSourcesAsync(FirmId firmId, UserId userId, SelfCostSourceRequestDto dto);
    }
}