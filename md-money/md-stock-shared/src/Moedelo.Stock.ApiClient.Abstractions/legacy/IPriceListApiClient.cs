using System.Threading.Tasks;
using System.Collections.Generic;
using Moedelo.Common.Types;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.PriceLists;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    public interface IPriceListApiClient
    {
        Task<PriceListDto> GetAsync(FirmId firmId, UserId userId, int id);

        Task<List<PriceListItemDto>> GetItemsAsync(FirmId firmId, UserId userId, int id, IReadOnlyCollection<long> stockProductIds);

        Task<PriceListInfoCollectionDto> GetListAsync(FirmId firmId, UserId userId, int pageNum = 1, int pageSize = 50, string name = null);
    }
}